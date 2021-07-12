using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Range(0,15)]
    [SerializeField] float movementSpeed = 5f;

    private bool moveRight = true;

    void Update()
    {
        if (moveRight)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
    }

    public void SetMovementDirectionToLeft()
    {
        moveRight = false;
    }
}
