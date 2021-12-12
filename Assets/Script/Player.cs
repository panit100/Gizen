using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType {MELEEATTACK,RANGEATTACK}

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Joystick joystick;
    public float moveSpeed = 10f;
    float horizontal = 0;
    float vertical = 0;
    Vector2 oldDiraction;
    Rigidbody2D rigidbody;

    [Header("Combat")]
    public int HP = 3;
    public float currentHitDelay = 0;
    public float hitDelay = 10f;
    public AttackType playerAttackType = AttackType.MELEEATTACK;
    public Transform attackOrigin = null;
    public float attackRadius = 0.6f;
    public float damage = 0f;
    public float attackDelay = 2f;
    public LayerMask enemyLayer = 0;
    public Transform projectile;
    public ParticleSystem slashParticle;
    bool attemptedDodge = false;
    public bool attemptedAttack = false;
    public float timeUntilAttackReadied = 2f;
    public float timeUntilDodgeReadied = 3f;

    [Header("SwipeDetect")]
    public bool isSpecialAttackOn = false;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 direction2D;
    float startTime;
    float endTime;
    public float SpecialAttackTime = 0;
    public int killed = 0;
    public bool isCanUseSpecialAttack = false;
    public Image fillSpecialButton;
    InputManager inputManager;



    [Header("Other")]
    Animator animator;

    private void Awake() {
        inputManager = InputManager.Instance;
    }
    
    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>().GetComponent<Joystick>();
    }

    private void FixedUpdate() {
        Move();
        UpdateSpecialAttackButton();
        timeUntilAttackReadied -= Time.deltaTime;
        currentHitDelay -= Time.deltaTime;
    }

    public void Attack(){
        attemptedAttack = true;
        // if(playerAttackType == AttackType.MELEEATTACK){
        HandleMeleeAttack();
        // }else if(playerAttackType == AttackType.RANGEATTACK){
        //     HandleRangeAttack();
        // }
    }
    public void HitPlayer(){
        if(currentHitDelay <= 0){
            HP -= 1;
            currentHitDelay = hitDelay;
        }
        if(HP <= 0){
            //DIE
            FindObjectOfType<ScoreManager>().gameoverUI.SetActive(true);
            FindObjectOfType<ScoreManager>().SaveScore();
            Destroy(this.gameObject);
            return;
        }
    }

    void HandleMeleeAttack(){
        // attemptedAttack = Input.GetKeyDown(KeyCode.Mouse0);

        if(attemptedAttack && timeUntilAttackReadied <= 0){
            animator.SetTrigger("slash");
            Invoke("PlaySlashVFX",0.7f);
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(attackOrigin.position,attackRadius,enemyLayer);
            for(int i = 0; i < overlapColliders.Length; i++){
                IDamageable enemyAttributes = overlapColliders[i].GetComponent<IDamageable>();
                if(enemyAttributes != null){
                    enemyAttributes.ApplyDamage(damage);
                }
            }
            attemptedAttack = false;
            timeUntilAttackReadied = attackDelay;
        }
    }

    void HandleRangeAttack(){
        // if(overlapColliders != null){
        //     Vector3 faceDirection = overlapColliders.transform.position - transform.position;
        //     float angle = Mathf.Atan2(faceDirection.y,faceDirection.x) * Mathf.Rad2Deg;
        //     rigidbody.rotation = angle;
        // }

        // attemptedAttack = Input.GetKeyDown(KeyCode.Mouse0);

        if(attemptedAttack && timeUntilAttackReadied <= 0){
            Collider2D overlapColliders = Physics2D.OverlapCircle(attackOrigin.position,attackRadius,enemyLayer);
            if(overlapColliders != null){
                IDamageable enemyAttributes = overlapColliders.GetComponent<IDamageable>();
                if(enemyAttributes != null){
                    // enemyAttributes.ApplyDamage(damage);

                    Transform _projectile = Instantiate(projectile,attackOrigin.position,attackOrigin.rotation);
                    _projectile.GetComponent<Projectile>().SetTraget(overlapColliders.gameObject.transform);
        
                    
                    timeUntilAttackReadied = attackDelay;
                }
            }
            attemptedAttack = false;
        }else{
            timeUntilAttackReadied -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackOrigin != null){
            Gizmos.DrawWireSphere(attackOrigin.position,attackRadius);
        }
    }

    public void PlaySlashVFX(){
        slashParticle.Play();
    }


    //-------SpacialAttack--------
    private void OnEnable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    void SwipeStart(Vector2 position, float time){
        startPosition = position;
        startTime = time;
    }

    void SwipeEnd(Vector2 position, float time){
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    void DetectSwipe(){
        float distance =  Vector3.Distance(startPosition,endPosition);
        float totalTime = endTime - startTime;
        // if(distance >= minDistance && totalTime <= maxTime){
            // Debug.Log("Swipe Detection");
            // Debug.DrawLine(startPosition,endPosition,Color.red,5f);
            // Vector3 direction3D = endPosition - startPosition;
            // Vector2 direction2D = new Vector2(direction3D.x,direction3D.y).normalized;
        // }
        if(isSpecialAttackOn){
            HandleSpecialAttack();
        }
    }


    void HandleSpecialAttack(){
        RaycastHit2D hit2D = Physics2D.Linecast(startPosition,endPosition,enemyLayer);
        if(hit2D != false){
            Destroy(hit2D.transform.gameObject);
        }
    }

    void UpdateSpecialAttackButton(){
        fillSpecialButton.fillAmount = killed / 10f;
        if(killed >= 10){
            isCanUseSpecialAttack = true;
        }
    }

    public void SpecialAttack(){
        if(isCanUseSpecialAttack){
            TurnOnSpecialAttack();
        }
    }

    void TurnOnSpecialAttack(){
        Time.timeScale = 0.5f;
        isSpecialAttackOn = true;
        Invoke("TurnOffSpecialAttack",SpecialAttackTime);
    }
    void TurnOffSpecialAttack(){
        Time.timeScale = 1f;
        killed = 0;
        isSpecialAttackOn = false;
    }
    //----------------------------

    //-------------------Move by swipe detecttion----------------
    // [SerializeField]
    // private Coroutine coroutine;

    // private void OnEnable() {
    //     inputManager.OnStartTouch += SwipeStart;
    //     inputManager.OnAlreadyTouch += SwipePerformed;
    //     inputManager.OnEndTouch += SwipeEnd;
    // }

    // private void OnDisable() {
    //     inputManager.OnStartTouch -= SwipeStart;
    //     inputManager.OnAlreadyTouch -= SwipePerformed;
    //     inputManager.OnEndTouch -= SwipeEnd;
    // }

    // void SwipeStart(Vector2 position){
    //     startPosition = position;
    // }

    // void SwipePerformed(Vector2 position){
    //     endPosition = position;
    //     coroutine = StartCoroutine(currentEndPosition());
    // }

    // void SwipeEnd(Vector2 position){
    //     StopCoroutine(coroutine);
    //     rigidbody.velocity = Vector2.zero;
    // }

    // IEnumerator currentEndPosition(){
    //     while(true){
    //         endPosition = inputManager.PrimaryPosition();
    //         DectectSwipe();
    //         yield return null;
    //     }
    // }

    // void DectectSwipe(){
    //     Vector3 direction3D = endPosition - startPosition;
    //     direction2D = new Vector2(direction3D.x,direction3D.y).normalized;
    //     MoveBySwipe(direction2D);          
    // }
    // void MoveBySwipe(Vector2 direction){
    //     rigidbody.velocity = direction * moveSpeed;

    //     if(rigidbody.velocity != Vector2.zero){
    //         animator.SetBool("run",true);
    //     }else{
    //         animator.SetBool("run",false);
    //     }

    //     HandleRun(direction);
    // }

    // void HandleRun(Vector2 direction){
    //     if(direction.x >= 0){
    //         transform.rotation = Quaternion.Euler(0,0,0);
    //     }else if(direction.x < 0){
    //         transform.rotation = Quaternion.Euler(0,180f,0);
    //     }
    // }
    //-----------------------------------------------

    //---------------Move by Old input---------------
    void Move(){
        // joystick = GetComponent<Joystick>();

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Debug.Log(horizontal + "," + vertical);

        oldDiraction = new Vector2(horizontal,vertical);

        rigidbody.velocity = oldDiraction * moveSpeed;

        if(horizontal != 0 || vertical != 0){
            animator.SetBool("run",true);
        }else{
            animator.SetBool("run",false);
        }

        OldHandleRun();
    }

    void OldHandleRun(){
        if(horizontal > 0 && transform.rotation.y != 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(horizontal < 0 && transform.rotation.y == 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }
    }
    //----------------------------------------------
}
