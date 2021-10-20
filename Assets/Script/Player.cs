using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    float horizontal = 0;
    float vertical = 0;
    public float moveSpeed = 10f;
    Vector2 diraction;
    Rigidbody2D rigidbody;

    [Header("Combat")]
    public Transform attackOrigin = null;
    public float attackRadius = 0.6f;
    public float damage = 0f;
    public float attackDelay = 2f;
    public LayerMask enemyLayer = 0;

    bool attemptedDodge = false;
    bool attemptedAttack = false;
    public float timeUntilAttackReadied = 2f;
    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Move();

        HandleAttack();
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

    void HandleAttack(){
        attemptedAttack = Input.GetKeyDown(KeyCode.Mouse0);

        if(attemptedAttack && timeUntilAttackReadied <= 0){
            Debug.Log("Player : Attempted Attack");

            timeUntilAttackReadied = attackDelay;
            
        }else{
            timeUntilAttackReadied -= Time.deltaTime;
        }
    }
}
