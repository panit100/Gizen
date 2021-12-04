using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public GameObject score;
    public GameObject mainmenu;

    public void startGame(){
        SceneManager.LoadScene(1);
    }

    public void OpenScore(){
        score.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void ScoreBack(){
        score.SetActive(false);
        mainmenu.SetActive(true);
    }
}
