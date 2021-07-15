using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] int numberOfLives = 3;
    [SerializeField] Text livesDisplayText;
    [Header("Time")]
    [SerializeField] float timeLimit = 30f;
    [SerializeField] Slider timeDisplay;
    
    
    private void Start()
    {
        timeDisplay.maxValue = timeLimit;
        
        livesDisplayText.text = numberOfLives.ToString();
    }

    private void Update()
    {
        timeDisplay.value += Time.deltaTime;
        if (timeDisplay.value >= timeLimit)
        {
            LoseLife();
        }
    }
        
    public void LoseLife()
    {
        numberOfLives--;
        if (numberOfLives <= 0)
        {
            Debug.Log("GameOver");
        }
        livesDisplayText.GetComponent<Text>().text = numberOfLives.ToString();
        ResetTimer();
        FindObjectOfType<Player>().ResetPosition();
    }

    public void ResetTimer()
    {
        timeDisplay.value = 0;
    }

    public void LevelCompleted()
    {
        Debug.Log("Victory");
        
    }
}
