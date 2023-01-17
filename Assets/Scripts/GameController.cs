using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject headPrefab;
    public GameObject pnMenuStart;
    public GameObject pnMenuGameOver;
    public SpawnFood spawnFood;
    private bool gameStart;
    public Text scoreGame;
    public Text scoreGameOver;
    private int score;



    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        pnMenuStart.SetActive(true);
        pnMenuGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameStart == false)
        {
            StartGame();
        }

        scoreGame.text = "SCORE: "+score.ToString();
    }

    public void StartGame()
    {
        pnMenuStart.SetActive(false);
        gameStart = true;
        scoreGame.text = "SCORE: 0";
        score = 0;
        spawnFood.StartSpawnFood();
        //Instantiate(headPrefab, new Vector2(0f,0f), Quaternion.identity);
    }
    public void GameOver()
    {
        pnMenuGameOver.SetActive(true);
        scoreGameOver.text = "SCORE: "+score.ToString();
        Invoke("RestartGame", 5);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void IncScore()
    {
        score += 10;
    }
}
