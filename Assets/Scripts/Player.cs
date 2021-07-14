using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     // dziedziczenie - pola i metody zadeklarowane w MonoBehaviour mog¹ zostaæ u¿yte w Player
{
    private int leftPlayspaceBorder;        // enkapsulacja - dostêp 'z zewn¹trz' jest ograniczony
    private int rightPlayspaceBorder;       // modyfikator dostêpu private - pole jest dostêpne tylko dla metod klasy w której siê znajduje
    private int bottomPlayspaceBorder = 1;
    private int topPlayspaceBorder = 11;

    private Animator myAnimator;
    private Vector3 startingPosition;

    // konstruktor domyœlny - modyfikator dostêpu public, brak argumentów, inicjuje wartoœci pól zerem/false/null
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        startingPosition = transform.position;

        SetMovementBounds();
        
    }
    private void Update()
    {
        Move();
    }

    private void SetMovementBounds()
    {
        leftPlayspaceBorder = Mathf.CeilToInt(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);    // new - wywo³anie konstruktora struktury Vector3
        
        rightPlayspaceBorder = Mathf.FloorToInt(Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x);     // inne przeci¹¿enie konstruktora Vector3, parametry x i y, a z = 0
        
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
        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Logs")))
        {
            transform.parent = collision.gameObject.transform;
        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(transform.parent = null))
        {
            transform.parent = null;
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Logs"))) { return; }

        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            transform.position = startingPosition;
        }
        
        
    }

}
