using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : PlayerAttack
{
    public override void Attack(){
        base.Attack();
        HandleMeleeAttack();
    }

    void HandleMeleeAttack(){
        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(attackOrigin.position,attackRadius);

        foreach(var collider in overlapColliders)
        {
            if(collider.TryGetComponent<BaseEnemy>(out var enemy))
            {
                enemy.TakeDamage(GetComponent<PlayerManager>().Damage);
            }
        }

        GetComponent<PlayerManager>().Animator.SetTrigger("slash");
        Invoke("PlaySlashVFX",0.7f);
        elapsedTime = 0;
    }
}
