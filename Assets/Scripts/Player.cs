using UnityEngine;

public class Player : MonoBehaviour     // dziedziczenie - pola i metody zadeklarowane w MonoBehaviour moga zostac uzyte w Player
{
    [SerializeField] GameObject deathIconPrefab;
    [SerializeField] AudioClip frogDeathSound;
    [Range(0,1)]
    [SerializeField] float frogDeathSoundVolume = 0.5f;

    private int leftPlayspaceBorder;        // enkapsulacja - dostep 'z zewnatrz' jest ograniczony
    private int rightPlayspaceBorder;       // modyfikator dostepu private - pole jest dostepne tylko dla metod klasy w ktorej si? znajduje
    private int bottomPlayspaceBorder = 1;
    private int topPlayspaceBorder = 11;

    private const string HazardsLayerName = "Hazards";  // stringi przypisane do stalych, zeby unikac literowek
    private const string LogsLayerName = "Logs";

    private Animator myAnimator;
    private Vector3 startingPosition;
    private WinSpot[] winSpots;

    private int visitedLaneIndex;
    private int startingLaneIndex;

    // konstruktor domyslny - modyfikator dostepu public, brak argumentow, inicjuje wartosci pol zerem/false/null
    private void Start()
    {
        myAnimator = GetComponent<Animator>();      // referencja do animatora, zeby nie wykonywac GetComponent przy kazdym ruchu
        startingPosition = transform.position;      // zapisanie startowej pozycji zaby, zeby pojawiac sie w tym samym miejscu po stracie zycia
        startingLaneIndex = Mathf.RoundToInt(transform.position.y);
        visitedLaneIndex = startingLaneIndex;

        winSpots = FindObjectsOfType<WinSpot>();

        SetMovementBounds();
        
    }
    private void Update()
    {
        Move();

        if(transform.position.x > rightPlayspaceBorder || transform.position.x < leftPlayspaceBorder)           // hit za wyjechanie poza ekran
        {
            HandleHit();
        }
    }

    private void SetMovementBounds()                    // ograniczenie ruchu zaby lewo/prawo do brzegow kamery
    {
        leftPlayspaceBorder = Mathf.CeilToInt(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);    // new - wywolanie konstruktora struktury Vector3
        
        rightPlayspaceBorder = Mathf.FloorToInt(Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x);     // inne przeciazenie konstruktora Vector3, parametry x i y, a z = 0
        
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckForWinSpot()){ return; }
            if (transform.position.y < topPlayspaceBorder)
            {
                ProcessMove(Vector3.up, Quaternion.Euler(0, 0, 180));
                if (transform.position.y > visitedLaneIndex)
                {
                    AddPointsForNewLane();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))       // else if, zeby nie sprawdzac kolejnych strzalek jesli ktoras 'wyzej' jest wcisnieta
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

    private bool CheckForWinSpot()
    {
        foreach (var winspot in winSpots)
        {
            if (Vector3.Distance(winspot.transform.position, transform.position) <= 1.1f)
            {
                winspot.TryToMoveFrogToWinSpot();
                return true;
            }        
        }
        return false;
    }

    private void ProcessMove(Vector3 deltaPosition, Quaternion facingRotation)      // kod powtarzany kilka razy wyodrebniony jako oddzielna metoda
    {
        transform.position += deltaPosition;
        transform.rotation = facingRotation;
        myAnimator.SetTrigger("jumpTrigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask(LogsLayerName)))
        {
            transform.parent = collision.gameObject.transform;                   // przypisanie klody/zolwia jako rodzica zaby, zeby zsynchronizowac ich ruch w relacji do swiata
        
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
            HandleHit();
        }


    }

    public void HandleHit()
    {
        var deathIcon = Instantiate(deathIconPrefab, transform.position, Quaternion.identity);
        Destroy(deathIcon, 1);
        AudioSource.PlayClipAtPoint(frogDeathSound, Camera.main.transform.position, frogDeathSoundVolume);          // odtworzenie dzwieku z pozycji glownej kamery
        FindObjectOfType<GameController>().LoseLife();
        ResetPosition();
    }


    public void ResetPosition()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        transform.parent = null;
        visitedLaneIndex = startingLaneIndex;
    }

    private void AddPointsForNewLane()
    {
        FindObjectOfType<GameController>().AddScoreForLane();
        visitedLaneIndex++;
    }
}
