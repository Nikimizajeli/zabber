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
    [SerializeField] int pointsPerSecondLeft = 50;
    [SerializeField] int pointsPerLaneVisited = 50;
    [Header("Popup Canvas")]
    [SerializeField] GameObject levelCompletedCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] float delayOnLevelComplete = 3f;


    private List<int> scoreList;
    private int currentLevelScore;
    private int totalScore;

    private void Awake()                                                                    // wzorzec projektowy singleton
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
        levelCompletedCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        timeDisplay.maxValue = timeLimit;
        
        livesDisplayText.text = numberOfLives.ToString();

        scoreList = new List<int>();
        currentLevelScore = 0;
        totalScore = 0;
        scoreDisplay.text = currentLevelScore.ToString();
    }

    private void Update()
    {
        timeDisplay.value += Time.deltaTime;
        if (timeDisplay.value >= timeLimit)
        {
            FindObjectOfType<Player>().HandleHit();
        }
    }
        
    public void LoseLife()
    {
        numberOfLives--;
        if (numberOfLives <= 0)
        {
            Time.timeScale = 0;
            scoreList.Add(currentLevelScore);
            foreach(var score in scoreList)
            {
                totalScore += score;
            }

            gameOverCanvas.SetActive(true);
            gameOverCanvas.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Total score: " + totalScore.ToString();      // text z drugiego dziecka pierwszego dziecka
        }
        livesDisplayText.GetComponent<Text>().text = numberOfLives.ToString();
        ResetTimer();
    }

    public void ResetTimer()
    {
        timeDisplay.value = 0;
    }

    public void LevelCompleted()
    {
        scoreList.Add(currentLevelScore);
        StartCoroutine(HandleVictory());
        
    }

    IEnumerator HandleVictory()
    {
        var player = FindObjectOfType<Player>();
        Destroy(player);
        levelCompletedCanvas.SetActive(true);
        levelCompletedCanvas.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Level score: " + currentLevelScore.ToString();
        currentLevelScore = 0;
        yield return new WaitForSeconds(delayOnLevelComplete);
        levelCompletedCanvas.SetActive(false);
        FindObjectOfType<LevelLoader>().LoadNextScene();
        ResetTimer();
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

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }
}
