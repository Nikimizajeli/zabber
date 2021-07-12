using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
        
    public void StartMoving(Vector3 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
        
}
