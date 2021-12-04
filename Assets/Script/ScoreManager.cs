using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI scoreText1,scoreText2,scoreText3;
    public int score1,score2,score3;
    
    [Header("GameOver")]
    public GameObject gameoverUI;
    public TextMeshProUGUI gameoverScoreText;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreText != null){
            scoreText.text = "Score : " + score;
        }

        if(scoreText1 != null){
            GetScore();
        }

        if(gameoverScoreText != null){
            GameOverText();
        }
    }

    public void AddScore(){
        score++;
    }
    public void SaveScore(){
        int lastscore = score;

        if(lastscore > PlayerPrefs.GetInt("Score3",0)){
            PlayerPrefs.SetInt("Score3",lastscore);
            if(lastscore > PlayerPrefs.GetInt("Score2",0)){
                int score2 = PlayerPrefs.GetInt("Score2",0);
                PlayerPrefs.SetInt("Score3",score2);
                PlayerPrefs.SetInt("Score2",lastscore);
                if(lastscore > PlayerPrefs.GetInt("Score1",0)){
                    int score1 = PlayerPrefs.GetInt("Score1",0);
                    PlayerPrefs.SetInt("Score2",score1);
                    PlayerPrefs.SetInt("Score1",lastscore);
                }
                
            }
            
        }

        PlayerPrefs.Save();
    }

    public void GetScore(){
        score1 = PlayerPrefs.GetInt("Score1",0);
        score2 = PlayerPrefs.GetInt("Score2",0);
        score3 = PlayerPrefs.GetInt("Score3",0);

        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
        scoreText3.text = score3.ToString();

    }

    public void ResetScore(){
        PlayerPrefs.DeleteAll();
    }

    void GameOverText(){
        gameoverScoreText.text = "Score : " + score;
        highScoreText.text = "HighScore : " + PlayerPrefs.GetInt("Score1",0);
    }
}
