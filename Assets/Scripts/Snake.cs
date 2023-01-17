using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public Vector2 dir = Vector2.right;
    private Vector2 input;

    bool ate = false;

    public GameObject tailPrefab;
    
    List<Transform> tails = new List<Transform>();

    GameObject gameController;

    private float speed;
    private float startTime;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        //InvokeRepeating("Move", 0.3f, 0.3f);
        speed = 0.2f;
        startTime = Time.time;
        StartCoroutine("NewMove");
    }

    // Update is called once per frame
    void Update()
    {
        if(dir.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                dir = Vector2.up;
                input = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                dir = Vector2.down;
                input = Vector2.down;
            }
        }
        else if(dir.y != 0f)
        {
             if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                dir = Vector2.right;
                input = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                dir = Vector2.left;
                input = Vector2.left;
            }
        }
    }
    void Move()
    {
        Vector2 atualCoordinate = transform.position;
        transform.Translate(dir);
        if(ate)
        {
            GameObject newtails = (GameObject)Instantiate(tailPrefab, atualCoordinate, Quaternion.identity);
            tails.Insert(0,newtails.transform);
            ate = false;
        }
        else if(tails.Count>0)
        {
            tails[tails.Count-1].position = atualCoordinate;
            tails.Insert(0, tails[tails.Count-1]);
            tails.RemoveAt(tails.Count-1);
        }
    }


    public bool Occupies(float x, float y)
    {
    foreach (Transform tail in tails)
    {
        if (tail.position.x == x && tail.position.y == y) {
            return true;
        }
    }

    return false;
    }

    private IEnumerator NewMove()
    {
        while (true)
        {
            Move();
            yield return new WaitForSeconds(speed);
            if(Time.time - startTime > 25)
            {
                if(speed > 0.05f) speed -= 0.02f;
                startTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name.StartsWith("Food"))
        {
            ate = true;
            Destroy(coll.gameObject);
            gameController.GetComponent<GameController>().IncScore();
        }
        else if (coll.gameObject.CompareTag("Respawn"))
        {
            Vector3 position = transform.position;

            if (dir.x != 0f) {
                position.x = -coll.transform.position.x + dir.x;
            } else if (dir.y != 0f) {
                position.y = -coll.transform.position.y + dir.y;
            }

        transform.position = position;
        }
        else if(coll.gameObject.CompareTag("Obstacle"))
        {
            gameController.GetComponent<GameController>().GameOver();
        }

    }
}
