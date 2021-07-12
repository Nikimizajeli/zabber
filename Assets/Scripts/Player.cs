using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int leftPlayspaceBorder;
    private int rightPlayspaceBorder;
    private int bottomPlayspaceBorder = 1;
    private int topPlayspaceBorder = 11;

    private Animator myAnimator;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();

        SetMovementBounds();
        
    }
    private void Update()
    {
        Move();
    }

    private void SetMovementBounds()
    {
        leftPlayspaceBorder = Mathf.CeilToInt(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);
        
        rightPlayspaceBorder = Mathf.FloorToInt(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
             if (transform.position.y < topPlayspaceBorder)
             {
                 ProcessMove(Vector3.up, Quaternion.Euler(0, 0, 180));
             }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))       // else if, ¿eby nie sprawdzaæ kolejnych strza³ek jeœli któraœ 'wy¿ej' jest wciœniêta
        {
            if (transform.position.y > bottomPlayspaceBorder)
            {
                ProcessMove(Vector3.down, Quaternion.identity);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x < rightPlayspaceBorder)
            {
                ProcessMove(Vector3.right, Quaternion.Euler(0, 0, 90));
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftPlayspaceBorder)
            {
                ProcessMove(Vector3.left, Quaternion.Euler(0, 0, -90));
            }
        }
    }

    private void ProcessMove(Vector3 deltaPosition, Quaternion facingRotation)
    {
        transform.position += deltaPosition;
        transform.rotation = facingRotation;
        myAnimator.SetTrigger("jumpTrigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        
        
    }
}
