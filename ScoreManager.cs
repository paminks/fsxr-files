using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public float currentScore;
    public TextMeshProUGUI scoreText;
    private float highScore = 0;

    void Start()
    {
        currentScore = 0f;
        
    }

    void Update()
    {
        currentScore += Time.deltaTime;
        scoreText.text = "Score: " + currentScore.ToString("0.00");
        if (currentScore > highScore)
        {
            currentScore = highScore;
        }
       
    }
}
