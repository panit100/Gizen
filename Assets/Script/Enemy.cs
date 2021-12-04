using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDamageable
{
    public AIPath aIPath;
    SpriteRenderer sprite;


    public float maxHealth = 10f;
    public float outlineOpen = 3f;
    public float outlineClose = -10f;


    [Header("Status")]
    public float maxSpeed = 5f;
    public float speed = 0f;
    public float currentHealth = 10f;
    public float attackRadius;
    public bool attemptedAttack = false;

    public LayerMask playerLayer = 0;


    public Transform attackOrigin;

    public AIDestinationSetter aIDestinationSetter;
    ScoreManager score;


    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
        
        sprite = GetComponent<SpriteRenderer>();
        aIDestinationSetter.target = FindObjectOfType<Player>().GetComponent<Player>().transform;

        currentHealth = maxHealth;
        speed = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReachedToPlayer();
        HandleRun();
    }

    public virtual void ApplyDamage(float amount){
        HitColor(amount);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        score.AddScore();
        Destroy(this.gameObject);
    }

    void HitColor(float amount){
        StartCoroutine(changeColorRed());
        StartCoroutine(changeColorBack(amount));
    }
    IEnumerator changeColorRed(){
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.red;
    }
    IEnumerator changeColorBack(float amount){
        yield return new WaitForSeconds(0.75f);
        sprite.color = Color.white;
        currentHealth -= amount;

    }

    void ReachedToPlayer(){
        aIPath.maxSpeed = speed;

        if(aIPath.whenCloseToDestination == CloseToDestinationMode.Stop && aIPath.reachedEndOfPath){      
            speed = 0;
            StartCoroutine(Attack1());
        }
    }    

    IEnumerator Attack1(){
        // Debug.Log("Wait for Attack");

        if(attemptedAttack == false){
            attemptedAttack = true;
            yield return new WaitForSeconds(3f);
            if(attemptedAttack){
                Collider2D overlapCollider = Physics2D.OverlapCircle(attackOrigin.position,attackRadius,playerLayer);
                if(overlapCollider != null){
                    Player playerAttributes = overlapCollider.GetComponent<Player>();
                    if(playerAttributes != null){
                        Debug.Log("Attack by Enemy" + Time.time);
                        playerAttributes.HitPlayer();
                    }
                }
            }
            speed = maxSpeed;
            attemptedAttack = false;
        }
    }


    //ememy must turn to player all time
    void HandleRun(){
        if(aIPath.desiredVelocity.x > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(aIPath.desiredVelocity.x  < 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackOrigin != null){
            Gizmos.DrawWireSphere(attackOrigin.position,attackRadius);
        }
    }
}
