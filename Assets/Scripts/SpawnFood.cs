using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    // public Transform borderTop;
    // public Transform borderLeft;
    // public Transform borderRight;
    // public Transform borderBottom;

    public Collider2D gridArea;

    public GameObject foodPrefab;

    private Snake snake;

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Spawn()
    {

        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        while (snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = bounds.min.x;
                y++;

                if (y > bounds.max.y) {
                    y = bounds.min.y;
                }
            }
        }

        Instantiate(foodPrefab, new Vector2(x,y), Quaternion.identity);
    }

    public void StartSpawnFood()
    {
        InvokeRepeating("Spawn", 3, 4);
    }
}
