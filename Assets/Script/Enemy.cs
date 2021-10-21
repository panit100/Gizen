using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float maxHealth = 10f;
    public float outlineOpen = 3f;
    public float outlineClose = -10f;

    [Header("Status")]
    public float speed = 5f;
    public float currentHealth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ApplyDamage(float amount){
        currentHealth -= amount;
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Destroy(this.gameObject);
    }
    
}
