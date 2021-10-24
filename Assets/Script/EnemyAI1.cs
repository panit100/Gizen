using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI1 : MonoBehaviour
{
    public List<Vector2> ray = new List<Vector2>();
    public List<float> dot = new List<float>();
    public Player player;

    private void Start() {
        // player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void Update() {
        DotProductCheck();

        AIMove();
    }

    void DotProductCheck(){
        for(int i = 0; i < dot.Count; i++){
            Vector2 dirToPlyer = (player.transform.position - this.transform.position).normalized;

            dot[i] = Vector2.Dot(dirToPlyer.normalized,ray[i]); 
        }
    }
    
    private void OnDrawGizmosSelected() {
        for(int i = 0; i < ray.Count; i++){
            ray[i] = new Vector2(Mathf.Cos(i * 30 * Mathf.Deg2Rad),Mathf.Sin(i *30 * Mathf.Deg2Rad)).normalized;
            Gizmos.DrawRay(transform.position, ray[i]);
        }
    }

    void AIMove(){
        float mostValueDot = 0;
        foreach(float n in dot){
            if(n > mostValueDot){
                mostValueDot = n;
            }
        }

        for(int i = 0; i < dot.Count; i ++){
            if(mostValueDot == dot[i]){
                transform.Translate(ray[i] * Time.deltaTime);
            }
        }

    }
}
