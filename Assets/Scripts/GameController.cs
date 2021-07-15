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
    [Header("Score")] 
    [SerializeField] Text scoreDisplay;
    public int pointsPerSecondLeft = 50;
    public int pointsPerLaneVisited = 50;

    private List<int> totalScore;
    private int currentLevelScore;

    private void Awake()
    {
        int numberOfGameControllers = FindObjectsOfType<GameController>().Length;
        if (numberOfGameControllers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        timeDisplay.maxValue = timeLimit;
        
        livesDisplayText.text = numberOfLives.ToString();

        totalScore = new List<int>();
        currentLevelScore = 0;
        scoreDisplay.text = currentLevelScore.ToString();
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

    public void AddScoreForLane()
    {
        currentLevelScore += pointsPerLaneVisited;
        scoreDisplay.text = currentLevelScore.ToString();
    }

    public void AddScoreForTime()
    {
        int remainingTime = Mathf.FloorToInt(timeLimit - timeDisplay.value);
        currentLevelScore += pointsPerSecondLeft * remainingTime;
        scoreDisplay.text = currentLevelScore.ToString();
    }
}
