using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttack : PlayerAttack
{
    public Transform projectile;

    public override void Attack(){
        if(elapsedTime > attackDelay){
            base.Attack();
            HandleRangeAttack();
        }
    }

    void HandleRangeAttack(){
        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(attackOrigin.position,attackRadius);

        foreach(var collider in overlapColliders)
        {
            if(collider.TryGetComponent<BaseEnemy>(out var enemy))
            {
                Transform _projectile = Instantiate(projectile,attackOrigin.position,attackOrigin.rotation);
                _projectile.GetComponent<Projectile>().SetTraget(enemy.transform);
                _projectile.GetComponent<Projectile>().SetDamage(GetComponent<PlayerManager>().Damage);
                
                elapsedTime = 0;
                return;
            }
        }
    }
}
