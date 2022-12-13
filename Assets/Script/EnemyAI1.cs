using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI1 : MonoBehaviour
{
    public List<Vector2> ray = new List<Vector2>();
    public List<float> dot = new List<float>();
    public List<float> weight = new List<float>();
    public PlayerManager player;
    public float moveSpeed = 0;
    public float radius = 1;
    public Rigidbody2D rigidbody;
    public float rayLength = 1f;
    public float distBTWPlayer;

    public float getAwayDist = 1f;
    private void Start() {
        // player = FindObjectOfType<Player>().GetComponent<Player>();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        DotProductCheck();
        distBTWPlayer = Vector2.Distance(transform.position,player.transform.position);

   
        NormalAIMove();
        // ShapingAIMOve();
        
    }

    void DotProductCheck(){
        for(int i = 0; i < dot.Count; i++){
            Vector2 dirToPlyer = (player.transform.position - this.transform.position).normalized;

            
            dot[i] = Vector2.Dot(dirToPlyer.normalized,ray[i]/radius); 
            weight[i] = 1.0f - Mathf.Abs(dot[i] - 0.65f);

            Vector2 dirRay = new Vector2(transform.position.x + (ray[i] * 2).x,transform.position.y + (ray[i] * 2).y);
            // RaycastHit2D obstruction = Physics2D.Raycast(new Vector2(transform.position.x + ray[i].x,transform.position.y + ray[i].y), ray[i] * 2,1f);
            RaycastHit2D obstruction = Physics2D.Raycast(transform.position, ray[i] * 2,rayLength);
            
            if(obstruction == true){
                dot[i] = 0;
            }

            
        }
        // float distBTWPlayer = Vector2.Distance(ray[i],player.transform.position);
            // if(distBTWPlayer <= getAwayDist){
            //     dot[i] = 0;
            // }

        // float dist = Vector2.Distance(this.transform.position, player.transform.position);
        // if(dist < fixDist){
        //     moveSpeed = -moveSpeed;
        // }else{
        //     moveSpeed = Mathf.Abs(moveSpeed);
        // }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,radius);

        for(int i = 0; i < ray.Count; i++){
            ray[i] = new Vector2(Mathf.Cos(i * 30 * Mathf.Deg2Rad),Mathf.Sin(i *30 * Mathf.Deg2Rad)).normalized * radius;
            
            Gizmos.DrawRay(new Vector2(transform.position.x + ray[i].x,transform.position.y + ray[i].y), (ray[i] * 2).normalized * rayLength * Mathf.Abs(dot[i]));
            if(dot[i] == 0){
                Gizmos.color = Color.black;
            }else if(dot[i] > 0){
                Gizmos.color = Color.green;
            }else if(dot[i] < 0){
                Gizmos.color = Color.red;
            }   
        }

    }

    void NormalAIMove(){
        float mostValueDot = 0;

        foreach(float n in dot){
            if(n > mostValueDot){
                mostValueDot = n;
            }
        }

        for(int i = 0; i < dot.Count; i ++){
            if(mostValueDot == dot[i] && distBTWPlayer > getAwayDist){
                rigidbody.velocity = ray[i] * moveSpeed * Time.deltaTime;
            }
        }

    }

    void ShapingAIMOve(){
        float mostValueWeight = 0;
        foreach(float n in weight){
            if(n > mostValueWeight){
                mostValueWeight = n;
            }
        }

        for(int i = 0; i < dot.Count; i ++){
            if(mostValueWeight == weight[i]){
                rigidbody.velocity = ray[i] * moveSpeed * Time.deltaTime;
            }
        }
    }
}
