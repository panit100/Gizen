using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy :  BaseEnemy
{
    public AIPath aIPath;
    SpriteRenderer sprite;
    
    public float outlineOpen = 3f;
    public float outlineClose = -10f;


    [Header("Status")]
    
    public float attackRadius;
    public bool attemptedAttack = false;

    public LayerMask playerLayer = 0;


    public Transform attackOrigin;

    public AIDestinationSetter aIDestinationSetter;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        aIDestinationSetter.target = FindObjectOfType<PlayerController>().transform;

        InitCharacter();
    }

    void FixedUpdate()
    {
        ReachedToPlayer();
        HandleRun();
    }

    public override void TakeDamage(float amount){
        HitColor();
        base.TakeDamage(amount);
    }

    public override void Die(){
        ScoreManager.Inst.AddScore();
        base.Die();
    }

    void HitColor(){
        StartCoroutine(changeColorRed());
        StartCoroutine(changeColorBack());
    }

    IEnumerator changeColorRed(){
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.red;
    }
    IEnumerator changeColorBack(){
        yield return new WaitForSeconds(0.75f);
        sprite.color = Color.white;
    }

    void ReachedToPlayer(){
        aIPath.maxSpeed = MoveSpeed;

        if(aIPath.whenCloseToDestination == CloseToDestinationMode.Stop && aIPath.reachedEndOfPath){      
            MoveSpeed = 0;
            StartCoroutine(Attack());
        }
    }    

    IEnumerator Attack(){
        if(attemptedAttack == false){
            attemptedAttack = true;
            yield return new WaitForSeconds(3f);
            if(attemptedAttack){
                Collider2D overlapCollider = Physics2D.OverlapCircle(attackOrigin.position,attackRadius,playerLayer);
                if(overlapCollider != null){
                    PlayerHealth playerAttributes = overlapCollider.GetComponent<PlayerHealth>();
                    if(playerAttributes != null){
                        Debug.Log("Attack by Enemy" + Time.time);
                        playerAttributes.HitPlayer();
                    }
                }
            }
            MoveSpeed = MaxSpeed;
            attemptedAttack = false;
        }
    }

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
