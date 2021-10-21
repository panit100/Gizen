using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {MELEEATTACK,RANGEATTACK}

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    float horizontal = 0;
    float vertical = 0;
    Vector2 diraction;
    Rigidbody2D rigidbody;

    [Header("Combat")]
    public AttackType playerAttackType = AttackType.MELEEATTACK;
    public Transform attackOrigin = null;
    public float attackRadius = 0.6f;
    public float damage = 0f;
    public float attackDelay = 2f;
    public LayerMask enemyLayer = 0;
    public Transform projectile;

    bool attemptedDodge = false;
    bool attemptedAttack = false;
    public float timeUntilAttackReadied = 2f;
    
    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Move();

        if(playerAttackType == AttackType.MELEEATTACK){
            HandleMeleeAttack();
        }else if(playerAttackType == AttackType.RANGEATTACK){
            HandleRangeAttack();
        }
    }

    void Move(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        diraction = new Vector2(horizontal,vertical);

        rigidbody.velocity = diraction * moveSpeed;

        HandleRun();
    }

    void HandleRun(){
        if(horizontal > 0 && transform.rotation.y != 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(horizontal < 0 && transform.rotation.y == 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }
    }

    void HandleMeleeAttack(){
        attemptedAttack = Input.GetKeyDown(KeyCode.Mouse0);
    
        if(attemptedAttack && timeUntilAttackReadied <= 0){
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(attackOrigin.position,attackRadius,enemyLayer);
            for(int i = 0; i < overlapColliders.Length; i++){
                IDamageable enemyAttributes = overlapColliders[i].GetComponent<IDamageable>();
                if(enemyAttributes != null){
                    enemyAttributes.ApplyDamage(damage);
                }
            }

            timeUntilAttackReadied = attackDelay;
            
        }else{
            timeUntilAttackReadied -= Time.deltaTime;
        }
    }

    void HandleRangeAttack(){
        // if(overlapColliders != null){
        //     Vector3 faceDirection = overlapColliders.transform.position - transform.position;
        //     float angle = Mathf.Atan2(faceDirection.y,faceDirection.x) * Mathf.Rad2Deg;
        //     rigidbody.rotation = angle;
        // }

        attemptedAttack = Input.GetKeyDown(KeyCode.Mouse0);

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

            
        }else{
            timeUntilAttackReadied -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackOrigin != null){
            Gizmos.DrawWireSphere(attackOrigin.position,attackRadius);
        }
    }
}
