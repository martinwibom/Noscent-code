using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongLogics : MonoBehaviour
{
    public UILogics UI;
    public BallLogics BallLogics;
    public PlayerLogics PlayerLogics;
    public Timer Timer;
    public Score ScoreCounter;
    public Countdown CountdownLogics;
    
    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject clove;
    public GameObject orange;

    public GameObject spawnPoint;
    public GameObject player;
    public GameObject goal;
    
    GameObject ball;

    public int timeRemaining;

    public int goalCount;

    bool gameOver;
    bool restartRunning;

    public bool paused = true;
    public bool gamePlaying = false;

    Vector3 ballStartPosition;


    // Start is called before the first frame update
    void Start()
    {
        ballStartPosition = spawnPoint.transform.position;
        paused = true;
        gameOver = false;
        timeRemaining = 60;
    }
    
    public void Play()
    {   
        if(!gamePlaying && prefab != null)
        {
            BallLogics = ball.GetComponent<BallLogics>();
            // PlayerLogics.paused = false;
            // BallLogics.Launch();
            goalCount = 0;
            gamePlaying = true;
            paused = false;
            // Timer.ToggleTimer();
            // CountdownLogics.StartCoroutine("TwoSeconds");
            StartCoroutine("PlayCoroutine");
        }
    }

    IEnumerator PlayCoroutine()
    {
        CountdownLogics.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds(2);

        PlayerLogics.paused = false;
        BallLogics.Launch();
        // Timer.ToggleTimer();
        StartCoroutine("ToggleTimer");
    }

    IEnumerator ToggleTimer()
    {  
        while (true)
        {
            if (timeRemaining >= 0)
            {
                Timer.UpdateTime(timeRemaining);

                yield return new WaitForSeconds(1);

                timeRemaining--;
            } else {   

                if(restartRunning){
                    StopCoroutine("RestartCoroutine");
                    CountdownLogics.StopCoroutine("TwoSeconds");
                    CountdownLogics.ResetText();}
                
                GameOver();
                break;
            }
        }
    }

    void GameOver()
    {
        BallLogics.Pause();
        PlayerLogics.paused = true;
        UI.AnouncementText("Game Over");

    }

    public void selectGarlic()
    {
        prefab = garlic;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Garlic");
    }

    public void selectApple()
    {
        prefab = apple;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Apple");
    }
    
    public void selectSoap()
    {
        prefab = soap;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Soap");
    }

    public void selectCoffee()
    {
        prefab = coffee;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Coffee");
    }

    public void selectClove()
    {
        prefab = clove;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Clove");
    }

    public void selectOrange()
    {
        prefab = orange;
        SpawnBall();
        UI.StartCoroutine("SelectedScent", "Orange");
    }
    
    public void Pause()
    {
        BallLogics.Pause();
        PlayerLogics.paused = true;
    }
    
    public void SpawnBall()
    {
        if(ball != null)
        {
            Destroy(ball.gameObject);
        } 
        ball = Instantiate(prefab, ballStartPosition, Quaternion.identity);
    }

    public void Resume()
    {
        if(paused && gamePlaying)
        {
            BallLogics.Resume();
            PlayerLogics.paused = false;
        }
    }


    IEnumerator RestartCoroutine()
    {
        restartRunning = true;
        PlayerLogics.paused = true;
        PlayerLogics.ResetPosition();
        BallLogics.ResetBall();
        BallLogics.SetRandomPosition();
        CountdownLogics.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds(2);

        PlayerLogics.paused = false;
        BallLogics.Launch();
        restartRunning = false;
    }

    public void Goal()
    {
        goalCount++;
        ScoreCounter.UpdateScore(goalCount);
        StartCoroutine("RestartCoroutine");

    }

    public void Miss()
    {
        //Removed goal penalty
        // goalCount--;
        // ScoreCounter.RemoveScore();
        StartCoroutine("RestartCoroutine");
    }
}
