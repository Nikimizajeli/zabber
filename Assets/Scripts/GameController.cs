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

    bool triggeredLevelFinished = false;

    private void Start()
    {
        timeDisplay.maxValue = timeLimit;

        livesDisplayText.text = numberOfLives.ToString();
    }

    private void Update()
    {
        if (triggeredLevelFinished) { return; }
        
        timeDisplay.value += Time.deltaTime;
        if (timeDisplay.value >= timeLimit)
        {
            triggeredLevelFinished = true;
            LoseLife();
        }
    }

    public void LoseLife()
    {
        numberOfLives--;
        if(numberOfLives <= 0)
        {
            Debug.Log("GameOver");
        }
        livesDisplayText.GetComponent<Text>().text = numberOfLives.ToString();
        timeDisplay.value = 0;
        FindObjectOfType<Player>().ResetPosition();
    }
}
