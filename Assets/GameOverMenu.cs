using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject so;
    public GameObject scoreo;
    private TextMeshProUGUI score;
    private ScoreController scoreController;
    public void Show()
    {
        score = scoreo.GetComponent<TextMeshProUGUI>();
        scoreController = so.GetComponent<ScoreController>();
        gameObject.SetActive(true);
        score.text = "You got "+ scoreController.GetScore().ToString() + " points";
    }

    public void OnRestartButtonTap()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("level1");
    }
}
