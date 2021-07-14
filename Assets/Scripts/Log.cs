using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Vehicle
{

    public override void FlipHorizontally()     // polimorfizm - przesloniecie metody z klasy bazowej
    {
        // brak implementacji, bo sprite klody wyglada lepiej bez odwracania
        return;
    }

}
