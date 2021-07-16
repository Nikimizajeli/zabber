using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()                        // singleton, zeby muzyka nie przeskakiwala na poczatek przy zmianie levelu
    {
        int numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numberOfMusicPlayers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
