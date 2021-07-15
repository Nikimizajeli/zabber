using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSpot : MonoBehaviour
{
    [SerializeField] GameObject winMarkerPrefab;
    

    private static int winSpotsNumber;          // pole statyczne, wartosc wspolna dla wszystkich instancji
    private static int winSpotsWithFrogs = 0;
    private bool hasFrog = false;

    private GameController gameController;

    private void Start()
    {
        winSpotsNumber = FindObjectsOfType<WinSpot>().Length;
        gameController = FindObjectOfType<GameController>();
    }

    public void TryToMoveFrogToWinSpot()
    {
        if (hasFrog) { return; }
        winSpotsWithFrogs++;
        Instantiate(winMarkerPrefab, transform.position, Quaternion.identity);
        var remainingTime = gameController.GetRemainingTime();
        var pointsPerSecondLeft = gameController.pointsPerSecondLeft;
        gameController.AddScore(pointsPerSecondLeft * remainingTime);
        if (winSpotsWithFrogs >= winSpotsNumber)
        {
            gameController.LevelCompleted();
        }
        FindObjectOfType<Player>().ResetPosition();
        gameController.ResetTimer();
    }
}
