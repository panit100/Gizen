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

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ApplyDamage(float amount){
        HitColor(amount);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
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
    
}
