using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSpot : MonoBehaviour
{
    [SerializeField] GameObject winMarkerPrefab;
    

    private static int winSpotsNumber;          // pole statyczne, wartosc wspolna dla wszystkich instancji
    private static int winSpotsWithFrogs = 0;
    private bool hasFrog = false;

    private void Start()
    {
        winSpotsNumber = FindObjectsOfType<WinSpot>().Length;
    }

    public void TryToMoveFrogToWinSpot()
    {
        if (hasFrog) { return; }
        winSpotsWithFrogs++;
        Instantiate(winMarkerPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<GameController>().AddScoreForTime();
        if (winSpotsWithFrogs >= winSpotsNumber)
        {
            FindObjectOfType<GameController>().LevelCompleted();
        }
        FindObjectOfType<Player>().ResetPosition();
        FindObjectOfType<GameController>().ResetTimer();
    }
}
