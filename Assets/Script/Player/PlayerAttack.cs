using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Combat")]
    public AttackType playerAttackType = AttackType.MELEEATTACK;
    public Transform attackOrigin = null;
    public float attackRadius = 0.6f;
    public float attackDelay = 2f;
    public ParticleSystem particle;
    public bool attemptedAttack = false;
    public float elapsedTime = 0f;

    // bool attemptedDodge = false;
    [SerializeField] Weapon weapon;

    private void Awake() 
    {
        GetComponent<PlayerController>().OnAttack += Attack;    
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public virtual void Attack(){

    }

    private void OnDrawGizmosSelected() {
        if(attackOrigin != null){
            Gizmos.DrawWireSphere(attackOrigin.position,attackRadius);
        }
    }

    public void PlaySlashVFX(){
        if(particle != null)
            particle.Play();
    }
}
