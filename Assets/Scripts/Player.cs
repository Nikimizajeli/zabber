using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator myAnimator;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ProcessMove(Vector3.up, Quaternion.Euler(0,0,180));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))       // else if, ¿eby nie sprawdzaæ kolejnych strza³ek jeœli któraœ 'wy¿ej' jest wciœniêta
        {
            ProcessMove(Vector3.down, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ProcessMove(Vector3.right, Quaternion.Euler(0, 0, 90));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ProcessMove(Vector3.left, Quaternion.Euler(0, 0, -90));
        }
    }

    private void ProcessMove(Vector3 deltaPosition, Quaternion facingRotation)
    {
        transform.position += deltaPosition;
        transform.rotation = facingRotation;
        myAnimator.SetTrigger("jumpTrigger");
    }
}
