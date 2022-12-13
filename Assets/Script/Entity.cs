using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour, IHaveHealth, IHaveSpeed
{
    protected float currentHealth;
    protected float moveSpeed;

    public abstract float MaxHealth {get;}
    public float CurrentHealth { get {return currentHealth;} set {currentHealth = value;} }

    public abstract float MaxSpeed {get;}

    public float MoveSpeed { get {return moveSpeed;} set {moveSpeed = value;} }

    public abstract void InitCharacter();

    public abstract void TakeDamage(float amount);

    public abstract void Die();
}

public class BasePlayer : Entity
{
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float damage;

    float currentDamage;

    public override float MaxHealth {get {return health;}}
    public override float MaxSpeed {get {return speed;}}
    public float MaxDamage {get {return damage;}}
    public float Damage {get {return currentDamage;} set {currentDamage = value;}}

    public override void InitCharacter()
    {
        currentHealth = MaxHealth;
        moveSpeed = MaxSpeed;
        currentDamage = MaxDamage;
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
}

public class BaseEnemy : Entity
{
    [SerializeField] EnemySetting enemySetting;
    public override float MaxHealth {get {return enemySetting.Health;}}
    public override float MaxSpeed {get {return enemySetting.Speed;}}
    public AttackType EnemyType {get {return enemySetting.EnemyType;}}

    public override void InitCharacter()
    {
        CurrentHealth = MaxHealth;
        MoveSpeed = MaxSpeed;
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
}

public class BaseNPC : Entity
{
    // NPCSetting
    public override float MaxHealth => throw new NotImplementedException(); //{get {return NPCSetting.Health;}}
 
    public override float MaxSpeed => throw new NotImplementedException(); //{get {return NPCSetting.Speed;}} 


    public override void Die()
    {
        
    }

    public override void InitCharacter()
    {
        CurrentHealth = MaxHealth;
        MoveSpeed = MaxSpeed;
    }

    public override void TakeDamage(float amount)
    {
        
    }
}

public interface IHaveHealth : IDamageable
{
    public float MaxHealth {get ;}
    public float CurrentHealth {get ; set;}
}


public interface IHaveSpeed
{
    float MaxSpeed {get ;}
    float MoveSpeed {get ; set;}
}
