using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
        
    public void StartMoving(Vector3 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public virtual void FlipHorizontally()          // virtual, nie abstract - zebym m�g�, ale nie musia� zaimplementowa� metod� inaczej w klasie pochodnej
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

}
