using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontal {get; private set;}
    public float vertical {get; private set;}

    public event Action OnAttack = delegate {};

    PlayerManager playerManager;
    Vector2 oldDiraction;
    Rigidbody2D rigidbody;
    
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            OnAttack();
        }
    }

    void Move()
    {
        oldDiraction = new Vector2(horizontal,vertical);

        rigidbody.velocity = oldDiraction * playerManager.MoveSpeed;

        if(playerManager.Animator != null)
        {
            if(oldDiraction != Vector2.zero)
                playerManager.Animator.SetBool("run",true);
            else
                playerManager.Animator.SetBool("run",false);
        }

        HandleRun();
    }

    void HandleRun()
    {
        if(horizontal > 0 && transform.rotation.y != 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(horizontal < 0 && transform.rotation.y == 0){
            transform.rotation = Quaternion.Euler(0,180f,0);
        }
    }
}
