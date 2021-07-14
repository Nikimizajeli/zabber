using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Vehicle
{
    public void TurnOffCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void TurnOnCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
