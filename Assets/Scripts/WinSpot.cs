using UnityEngine;

public class WinSpot : MonoBehaviour
{
    [SerializeField] GameObject winMarkerPrefab;
    

    private static int winSpotsNumber;          // pole statyczne, wartosc wspolna dla wszystkich instancji, nie resetuje sie przy zmianie sceny
    private static int winSpotsWithFrogs;
    public bool hasFrog = false;

    private void Start()
    {
        winSpotsWithFrogs = 0;
        winSpotsNumber = FindObjectsOfType<WinSpot>().Length;
    }

    public void TryToMoveFrogToWinSpot()
    {
        if (hasFrog) { return; }
        winSpotsWithFrogs++;
        Instantiate(winMarkerPrefab, transform.position, Quaternion.identity);
        hasFrog = true;
        FindObjectOfType<GameController>().AddScoreForTime();
        if (winSpotsWithFrogs >= winSpotsNumber)
        {
            FindObjectOfType<GameController>().LevelCompleted();
            return;
        }
        FindObjectOfType<Player>().ResetPosition();
        FindObjectOfType<GameController>().ResetTimer();
    }
}
