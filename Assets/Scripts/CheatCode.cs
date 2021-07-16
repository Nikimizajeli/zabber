using UnityEngine;

public class CheatCode : MonoBehaviour
{
    [SerializeField] KeyCode[] cheatCode = { KeyCode.F, 
                                             KeyCode.A,
                                             KeyCode.B,
                                             KeyCode.R,
                                             KeyCode.Y,
                                             KeyCode.K,
                                             KeyCode.A};

    private int cheatIndex;

    private void Start()
    {
        cheatIndex = 0;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[cheatIndex]))
            {
                cheatIndex++;                           
                if(cheatIndex >= cheatCode.Length)
                {
                    FindObjectOfType<GameController>().LevelCompleted();            // wykonanie cheatu
                }
            }
            else
            {
                cheatIndex = 0;
            }
        }
    }
}
