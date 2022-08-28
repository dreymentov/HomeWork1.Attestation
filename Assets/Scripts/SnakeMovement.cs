using TMPro;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    public int Length = 1;
    public int CriclesSnake = 1;
    public int CriclesSnakeCorrent = 1;

    public TextMeshPro PointsText;

    private Camera mainCamera;
    private Rigidbody2D componentRigidbody;
    private SnakeTail componentSnakeTail;

    private Vector2 touchLastPos;
    private float sidewaysSpeed;

    public bool ifNeedCheck;

    public GameObject GameManager;

    private void Start()
    {
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody2D>();
        componentSnakeTail = GetComponent<SnakeTail>();
        GameManager = GameObject.Find("GameManager");

        for (int i = 0; i < Length; i++)
        {
            if(i % 10 == 0) 
            {
                componentSnakeTail.AddCircle();
                CriclesSnake++;
            }
        }

        PointsText.SetText(Length.ToString());
    }

    private void Update()
    {
        if(GameManager.gameObject.GetComponent<Gamemanagerscript>().isGaming)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                sidewaysSpeed = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 delta = (Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
                sidewaysSpeed += delta.x * Sensitivity;
                touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            }

            if (ifNeedCheck == true)
            {
                Invoke("CheckLenght", 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameObject.GetComponent<Gamemanagerscript>().isGaming)
        {
            if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
            componentRigidbody.velocity = new Vector2(sidewaysSpeed * 5, ForwardSpeed);

            sidewaysSpeed = 0;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    void CheckLenght()
    {
        if (GameManager.gameObject.GetComponent<Gamemanagerscript>().isGaming)
        {
            Debug.Log("Checked " + Length);
            ifNeedCheck = false;

            CriclesSnakeCorrent = 0;

            for (int i1 = 0; i1 < Length; i1++)
            {
                if (i1 % 10 == 0)
                {
                    CriclesSnakeCorrent++;
                }
            }

            if (CriclesSnakeCorrent < CriclesSnake)
            {
                for (int i2 = 0; i2 < CriclesSnake - CriclesSnakeCorrent; i2++)
                {
                    componentSnakeTail.RemoveCircle();
                }
                CriclesSnake = CriclesSnakeCorrent;
                Debug.Log(CriclesSnake);
            }

            if (CriclesSnakeCorrent > CriclesSnake)
            {
                for (int i2 = 0; i2 < CriclesSnakeCorrent - CriclesSnake; i2++)
                {
                    componentSnakeTail.AddCircle();
                }
                CriclesSnake = CriclesSnakeCorrent;
                Debug.Log(CriclesSnake);
            }
        }
    }
}
