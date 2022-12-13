using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType {MELEEATTACK,RANGEATTACK}

public class PlayerManager : BasePlayer
{
    // [Header("SwipeDetect")]
    // public bool isSpecialAttackOn = false;
    // public Vector2 startPosition;
    // public Vector2 endPosition;
    // public Vector2 direction2D;
    // float startTime;
    // float endTime;
    // public float SpecialAttackTime = 0;
    // public int killed = 0;
    // public bool isCanUseSpecialAttack = false;
    // public Image fillSpecialButton;
    // InputManager inputManager;

    Animator animator;
    public Animator Animator {get {return animator;}}

    private void Start() 
    {
        
        TryGetComponent<Animator>(out animator);
        InitCharacter();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    public override void Die()
    {
        ScoreManager.Inst.OnGameOver();
        base.Die();
    }



    //-------SpacialAttack--------
    // private void OnEnable() {
    //     inputManager.OnStartTouch += SwipeStart;
    //     inputManager.OnEndTouch += SwipeEnd;
    // }

    // private void OnDisable() {
    //     inputManager.OnStartTouch -= SwipeStart;
    //     inputManager.OnEndTouch -= SwipeEnd;
    // }

    // void SwipeStart(Vector2 position, float time){
    //     startPosition = position;
    //     startTime = time;
    // }

    // void SwipeEnd(Vector2 position, float time){
    //     endPosition = position;
    //     endTime = time;
    //     DetectSwipe();
    // }

    // void DetectSwipe(){
    //     float distance =  Vector3.Distance(startPosition,endPosition);
    //     float totalTime = endTime - startTime;
    //     // if(distance >= minDistance && totalTime <= maxTime){
    //         // Debug.Log("Swipe Detection");
    //         // Debug.DrawLine(startPosition,endPosition,Color.red,5f);
    //         // Vector3 direction3D = endPosition - startPosition;
    //         // Vector2 direction2D = new Vector2(direction3D.x,direction3D.y).normalized;
    //     // }
    //     if(isSpecialAttackOn){
    //         HandleSpecialAttack();
    //     }
    // }


    // void HandleSpecialAttack(){
    //     RaycastHit2D hit2D = Physics2D.Linecast(startPosition,endPosition,enemyLayer);
    //     if(hit2D != false){
    //         Destroy(hit2D.transform.gameObject);
    //     }
    // }

    // void UpdateSpecialAttackButton(){
    //     fillSpecialButton.fillAmount = killed / 10f;
    //     if(killed >= 10){
    //         isCanUseSpecialAttack = true;
    //     }
    // }

    // public void SpecialAttack(){
    //     if(isCanUseSpecialAttack){
    //         TurnOnSpecialAttack();
    //     }
    // }

    // void TurnOnSpecialAttack(){
    //     Time.timeScale = 0.5f;
    //     isSpecialAttackOn = true;
    //     Invoke("TurnOffSpecialAttack",SpecialAttackTime);
    // }
    // void TurnOffSpecialAttack(){
    //     Time.timeScale = 1f;
    //     killed = 0;
    //     isSpecialAttackOn = false;
    // }
    
    //----------------------------------------------
}
