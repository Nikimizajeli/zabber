using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     // dziedziczenie - pola i metody zadeklarowane w MonoBehaviour mog� zosta� u�yte w Player
{
    private int leftPlayspaceBorder;        // enkapsulacja - dost�p 'z zewn�trz' jest ograniczony
    private int rightPlayspaceBorder;       // modyfikator dost�pu private - pole jest dost�pne tylko dla metod klasy w kt�rej si� znajduje
    private int bottomPlayspaceBorder = 1;
    private int topPlayspaceBorder = 11;

    private const string HazardsLayerName = "Hazards";  // stringi przypisane do sta�ych, zeby unika� liter�wek
    private const string LogsLayerName = "Logs";

    private Animator myAnimator;
    private Vector3 startingPosition;

    // konstruktor domy�lny - modyfikator dost�pu public, brak argument�w, inicjuje warto�ci p�l zerem/false/null
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

    private void SetMovementBounds()                    // ograniczenie ruchu �aby lewo/prawo do brzeg�w kamery
    {
        leftPlayspaceBorder = Mathf.CeilToInt(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);    // new - wywo�anie konstruktora struktury Vector3
        
        rightPlayspaceBorder = Mathf.FloorToInt(Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x);     // inne przeci��enie konstruktora Vector3, parametry x i y, a z = 0
        
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
        else if (Input.GetKeyDown(KeyCode.DownArrow))       // else if, �eby nie sprawdza� kolejnych strza�ek je�li kt�ra� 'wy�ej' jest wci�ni�ta
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

    private void ProcessMove(Vector3 deltaPosition, Quaternion facingRotation)      // kod powtarzany kilka razy wyodr�bniony jako oddzielna metoda
    {
        transform.position += deltaPosition;
        transform.rotation = facingRotation;
        myAnimator.SetTrigger("jumpTrigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask(LogsLayerName)))
        {
            transform.parent = collision.gameObject.transform;                   // przypisanie k�ody/��wia jako rodzica �aby, �eby zsynchronizowa� ich ruch w relacji do �wiata
        
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
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask(LogsLayerName))) { return; }

        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask(HazardsLayerName)))
        {
            transform.position = startingPosition;
        }
        
        
    }

}
