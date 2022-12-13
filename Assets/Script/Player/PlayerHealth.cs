using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float elapsedTime = 0;
    public float hitDelayTime = 10f;

    PlayerManager playerManager;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void FixedUpdate() 
    {
        HitDelay();
    }

    void HitDelay()
    {
        elapsedTime += Time.deltaTime;
    }

    public void HitPlayer(){
        if(elapsedTime > hitDelayTime){
            playerManager.TakeDamage(1);
            elapsedTime = 0;
        }
    }
}
