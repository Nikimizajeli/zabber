using UnityEngine;
using UnityEngine.UI;

public class CreditsScore : MonoBehaviour
{
    void Start()
    {
        if(FindObjectOfType<GameController>() == null) { return; }
        GetComponent<Text>().text = "Total score: " + FindObjectOfType<GameController>().GetTotalScore().ToString();       
    }


}
