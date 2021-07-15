using System.Collections;
using System.Collections.Generic;
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
                    FindObjectOfType<LevelLoader>().LoadNextScene();            // wykonanie cheatu
                }
            }
            else
            {
                cheatIndex = 0;
            }
        }
    }
}