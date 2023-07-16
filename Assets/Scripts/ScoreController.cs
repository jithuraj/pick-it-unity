using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject scoreObject;
    private TextMeshProUGUI score;

    void Start()
    {
        score = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        score.text = ((int.Parse( score.text))+1).ToString()  ;
    }
}
