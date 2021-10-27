using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minDistance = 0.2f;
    [SerializeField]
    private float maxTime = 1f;
    
    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    
    private void Awake() {
        inputManager = InputManager.Instance;
    }

    private void OnEnable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    void SwipeStart(Vector2 position, float time){
        Debug.Log("Start = " + position);
        startPosition = position;
        startTime = time;
    }

    void SwipeEnd(Vector2 position, float time){
        Debug.Log("End = " + position);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    void DetectSwipe(){
        float distance =  Vector3.Distance(startPosition,endPosition);
        float totalTime = endTime - startTime;
        if(distance >= minDistance && totalTime <= maxTime){
            Debug.Log("Swipe Detection");
            Debug.DrawLine(startPosition,endPosition,Color.red,5f);
        }
    }

}
