using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject so;
    public GameObject hso;
    private TextMeshProUGUI scoreComponent;
    private TextMeshProUGUI highScoreComponent;
    private int currentScore = 0;
    private int highScore;

    void Start()
    {
        scoreComponent = so.GetComponent<TextMeshProUGUI>();
        highScoreComponent = hso.GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        currentScore = int.Parse(scoreComponent.text);
        currentScore++;
        scoreComponent.text = currentScore.ToString();
        UpdateHighScore();
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
}
