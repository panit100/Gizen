using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHaveHealth
{
    [SerializeField] float health;
    float currentHealth;
    public float MaxHealth {get {return health;}}
    public float CurrentHealth { get {return currentHealth;} set {currentHealth = value;} }

    void InitObstacle()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}