using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Vehicle
{
    public void TurnOffCollider()               // modyfikator dostêpu public - brak ograniczen, metoda dostepna z zewnatrz klasy, bedzie wywolana jako animation event w Turtle Dive
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void TurnOnCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
