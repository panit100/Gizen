using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed = 10f;
    public float damge = 1f;
    public float delaySeconds = 3f;

    WaitForSeconds cullDelay = null;
    public int enemyLayer = 3;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        cullDelay = new WaitForSeconds(delaySeconds);
        StartCoroutine(DelayCull());

        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.position,speed);
    }

    public void SetTraget(Transform _target){
        target = _target;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit Something");
        if(other.gameObject.layer == enemyLayer){
            Debug.Log("Hit Enemy");
            IDamageable enemyAttribute = other.GetComponent<IDamageable>();
            if(enemyAttribute != null){
                enemyAttribute.ApplyDamage(damge);
            }

            Destroy(this.gameObject);
        }
    }

    IEnumerator DelayCull(){
        yield return cullDelay;
        Destroy(this.gameObject);
    }
}
