using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject so;
    public GameObject hso;
    public GameObject lo1;
    private TextMeshProUGUI scoreComponent;
    private TextMeshProUGUI highScoreComponent;
    private TextMeshProUGUI lifeComponent;
    private int currentScore = 0;
    private int highScore;
    private int life=3;
    public GameOverMenu gameOverMenu;

    void Start()
    {
        scoreComponent = so.GetComponent<TextMeshProUGUI>();
        highScoreComponent = hso.GetComponent<TextMeshProUGUI>();
        lifeComponent = lo1.GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        currentScore = int.Parse(scoreComponent.text);
        currentScore++;
        scoreComponent.text = currentScore.ToString();
        UpdateHighScore();
    }

    public void DecreaseLife()
    {
        life--;
        lifeComponent.text = life.ToString();
        if(life <= 0)
        {
            gameOverMenu.Show();
        }
    }

    private void UpdateHighScore()
    {
        if(highScore <= currentScore)
        {
        PlayerPrefs.SetInt("high_score", currentScore);
        }
        ShowHighScore();
    }

    public void ShowHighScore()
    {
        highScore = PlayerPrefs.GetInt("high_score");
        highScoreComponent.text = highScore.ToString();
    }

    public int GetScore()
    {
        return currentScore;
    }
}
