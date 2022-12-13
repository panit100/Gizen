using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed = 10f;
    public float damage = 1f;
    public float timeToDestroy = 1f;

    Transform target;

    void Start()
    {
        StartCoroutine(DestroyProjectile());
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.position,speed);
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetTraget(Transform _target){
        target = _target;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.Equals(target)){
            target.GetComponent<BaseEnemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyProjectile(){
        yield return new WaitUntil(() => target.Equals(null));
        Destroy(this.gameObject);
    }
}
