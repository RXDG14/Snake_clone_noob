using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    List<GameObject> Tail = new List<GameObject>();
    List<Vector2> PositionHistory = new List<Vector2>();
    
    public GameObject tailPrefab;

    Vector2 dir = Vector2.right;
    bool ate = false;

    void Start()
    {
        InvokeRepeating("Movement", 0.5f, 0.5f);
    }

    void Update()
    {
        Direction();

    }

    private void Direction()
    {
        if (Input.GetKey(KeyCode.W) && dir != Vector2.down)
        {
            dir = Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) && dir != Vector2.right)
        {
            dir = Vector2.left;
        }
        if (Input.GetKey(KeyCode.S) && dir != Vector2.up)
        {
            dir = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) && dir != Vector2.left)
        {
            dir = Vector2.right;
        }
    }

    private void Movement()
    {
        int index = 1;
        transform.Translate(dir);
        PositionHistory.Insert(0, transform.position);//store head position and put it at the top of list
        foreach(var body in Tail)
        {
            Vector2 point = PositionHistory[index];
            body.transform.position = point;
            index++;
        }
        if (ate)
        {
            GrowSnake();
        }
        ate = false;
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(tailPrefab);
        Tail.Add(body);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Adder")
        {
            foreach (var body in Tail)
            {
                if (Tail.Contains(other.gameObject))
                {
                    //Destroy(body.gameObject);
                    Debug.Log("quit");
                }
            }
            ate = true;
            Destroy(other.gameObject);
        }
    }
}
    /*
     codemonkey tutorial:

    List<Vector2Int> snakeMovePositionList;

    [SerializeField] float speed = 0.1f;
    [SerializeField] GameObject tailPrefab;
    Vector2Int gridPosition;
    Vector2Int gridDirection;
    float gridMoveTimer;
    float gridMoveTimerMax;
    int snakeBodySize;
    bool ate = false;

    private void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
        gridDirection = new Vector2Int(1, 0);
        gridMoveTimerMax = 0.5f;
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridDirection.y != -1)
            {
                gridDirection.x = 0;
                gridDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridDirection.y != 1)
            {
                gridDirection.x = 0;
                gridDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridDirection.x != 1)
            {
                gridDirection.x = -1;
                gridDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridDirection.x != -1)
            {
                gridDirection.x = 1;
                gridDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            snakeMovePositionList.Insert(0, gridPosition);
            gridPosition += gridDirection;
            if (ate)
            {
                snakeBodySize++;
            }
            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }
            transform.position = new Vector3(gridPosition.x, gridPosition.y);


        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Adder")
        {
            ate = true;
            Destroy(other.gameObject);
        }
    }
}
*/

/*     //this is my old code which i did myself and it worked too , i just did it the other way above
       //because i liked it more.
    
    //Vector2 dir = Vector2.right;
    //bool keepGoingRight = false;
    //bool keepGoingLeft = false;
    //bool keepGoingUp = false;
    //bool keepGoingDown = false;
    
    void Start()
    {
        InvokeRepeating("Movement", 0.5f, 0.1f);
    }

    void Update()
    {
        Direction();
        //Movement();
    }

    private void Direction()
    {
        if (Input.GetKey(KeyCode.W) && dir != Vector2.down)// && keepGoingDown == false)
        {
            //MakeAllFalse();
            //keepGoingUp = !keepGoingUp;
            dir = Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) && dir != Vector2.right)// && keepGoingRight == false)
        {
            //MakeAllFalse();
            //keepGoingLeft = !keepGoingLeft;
            dir = Vector2.left;
        }
        if (Input.GetKey(KeyCode.S) && dir != Vector2.up)// && keepGoingUp == false)
        {
            //MakeAllFalse();
            //keepGoingDown = !keepGoingDown;
            dir = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) && dir != Vector2.left)// && keepGoingLeft == false)
        {
            //MakeAllFalse();
            //keepGoingRight = !keepGoingRight;
            dir = Vector2.right;
        }
    }

    private void Movement()
    {
        transform.Translate(dir);
        /*
        if (keepGoingUp == true)
        {
            //transform.Translate(Vector2.up * speed * Time.deltaTime);
            //transform.position += new Vector3(0, 1, 0);
            transform.Translate(Vector2.up);
        }
        if (keepGoingDown == true)
        {
            //transform.Translate(Vector2.down * speed * Time.deltaTime);
            //transform.position += new Vector3(0, -1, 0);
            transform.Translate(Vector2.down);
        }
        if (keepGoingLeft == true)
        {
            //transform.Translate(Vector2.left * speed * Time.deltaTime);
            //transform.position += new Vector3(-1, 0, 0);
            transform.Translate(Vector2.left);
        }
        if(keepGoingRight == true)
        {
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
            //transform.position += new Vector3(1, 0, 0);
            transform.Translate(Vector2.right);
        }
    }

    private void MakeAllFalse()
    {
        keepGoingRight = false;
        keepGoingLeft = false;
        keepGoingUp = false;
        keepGoingDown = false;
    }
}
*/
