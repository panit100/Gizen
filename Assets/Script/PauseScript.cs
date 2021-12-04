using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Pause(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Mainmenu(){
        SceneManager.LoadScene(0);
    }
}
