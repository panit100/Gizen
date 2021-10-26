using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{
    [SerializeField] protected float _maxSpeed, maxForce;
    protected float maxSpeed;
    protected Vector3 acceleration, velocity, location, startPosition;

    [SerializeField] Transform target;

    public Vector2 ahead1,ahead2;
    public float maxSeeAhead = 1;
    public float maxAvoidanceForce= 10;
    public float maxVelocity = 1;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        location = transform.position;
        maxSpeed = _maxSpeed;
        startPosition = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        steer(target.position);
        AppleSteeringToMotion();
    }

    void AppleSteeringToMotion(){
        velocity = Vector3.ClampMagnitude(velocity + acceleration,maxSpeed);

        location += velocity * Time.deltaTime;

        acceleration = Vector3.zero;

        RotateTowardTarget();

        transform.position = location;
    }

    void steer(Vector3 targetPosition){
        Vector3 desiredVelocity = targetPosition - location;

        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - velocity, maxForce);

        steer += collisionAvoidance();

        ApplyForce(steer);
    }

    void ApplyForce(Vector3 force){
        acceleration += force;
    }

    void RotateTowardTarget(){
        Vector3 directionToDesiredLocation = location - transform.position;

        directionToDesiredLocation.Normalize();

        float rotZ = Mathf.Atan2(directionToDesiredLocation.y,directionToDesiredLocation.x) * Mathf.Rad2Deg;
        rotZ -= 90;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position,ahead1);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,ahead2);
        Gizmos.color = Color.red;
    }

    float DistanceBTWObtacle(Vector3 obstacle,Vector3 aheadLine){
        return Mathf.Sqrt((obstacle.x - aheadLine.x) * (obstacle.x - aheadLine.x) + (obstacle.y - aheadLine.y) * (obstacle.y - aheadLine.y));
    }

    bool LineIntersectsCircle(Vector3 ahead1,Vector3 ahead2, Transform obstacle){
        return DistanceBTWObtacle(obstacle.position,ahead1) <= obstacle.gameObject.GetComponent<CircleCollider2D>().radius || DistanceBTWObtacle(obstacle.position,ahead2) <= obstacle.gameObject.GetComponent<CircleCollider2D>().radius;
    }

    Vector3 collisionAvoidance(){
        float velocityLength = Mathf.Abs(Mathf.Pow(velocity.x,2) + Mathf.Pow(velocity.y,2));
        float dynamicLength = velocityLength / maxVelocity;
        ahead1 = transform.position + velocity.normalized * dynamicLength;
        ahead2 = transform.position + velocity.normalized * maxSeeAhead * 0.5f;

        Transform mostThreatening = fineMostThreateningObstacle();
        Vector3 avoidance = Vector3.zero;

        if(mostThreatening != null){
            avoidance.x = ahead1.x - mostThreatening.position.x;
            avoidance.y = ahead1.y- mostThreatening.position.y;

            avoidance.Normalize();
            avoidance *= maxAvoidanceForce;
        }else{
            avoidance *= 0;
        }

        return avoidance;
    }

    Transform fineMostThreateningObstacle(){
        Transform mostThreatening = null;

        Collider2D[] obstruction = Physics2D.OverlapCircleAll(transform.position, maxSeeAhead);

        for(int i = 0; i < obstruction.Length; i++){
            bool intersect = LineIntersectsCircle(ahead1,ahead2,obstruction[i].transform);

            if(intersect && (mostThreatening == null)){
                mostThreatening = obstruction[i].transform;
            }
        }

        return mostThreatening;

    }

}
