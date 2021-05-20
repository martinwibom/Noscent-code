using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongLogics : MonoBehaviour
{
    public UILogics UI;
    public BallLogics BallLogics;
    public PlayerLogics PlayerLogics;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;
    
    public GameObject prefab;
    public GameObject rose;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject vanilla;
    public GameObject orange;

    public GameObject spawnPoint;
    public GameObject player;
    public GameObject goal;
    
    GameObject ball;

    public int timeRemaining;
    public int goalCount;

    public float ballSpeed;

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
        UI.StartCoroutine("TwoSeconds");
        PlayPanelScript.gameObject.SetActive(false);


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
                UI.UpdateTime(timeRemaining);

                yield return new WaitForSeconds(1);

                timeRemaining--;
            } else {   

                if(restartRunning){
                    StopCoroutine("RestartCoroutine");
                    UI.StopCoroutine("TwoSeconds");
                    UI.cdText.text = "";
                }
                
                GameOver();
                break;
            }
        }
    }

    void EasyDifficulty()
    {
        ballSpeed = 5f;
        goal.transform.localScale = new Vector3(goal.transform.localScale.x, 200, 0);
    }

    void MediumDifficulty()
    {
        ballSpeed = 8f;
        goal.transform.localScale = new Vector3(goal.transform.localScale.x, 175, 0);
    }

    void HardDifficulty()
    {
        ballSpeed = 10f;
        goal.transform.localScale = new Vector3(goal.transform.localScale.x, 150, 0);
    }

    void GameOver()
    {
        BallLogics.Pause();
        PlayerLogics.paused = true;
        UI.AnnouncementText("Game Over");
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
        UI.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds(2);

        PlayerLogics.paused = false;
        BallLogics.Launch();
        restartRunning = false;
    }

    public void Goal()
    {
        goalCount++;
        UI.UpdateScore(goalCount);
        StartCoroutine("RestartCoroutine");

    }

    public void Miss()
    {
        //Removed goal penalty
        // goalCount--;
        // ScoreCounter.RemoveScore();
        StartCoroutine("RestartCoroutine");
    }

    public void CheckDifficulty()
    {
        if(ScentOptionScript.easySelected)
        {
            EasyDifficulty();
        } else if (ScentOptionScript.mediuSelected)
        {
            MediumDifficulty();
        } else if (ScentOptionScript.hardSelected)
        {
            HardDifficulty();
        }
    }

    public void CheckScent()
    {
        if(ScentOptionScript.roseSelected)
        {
            prefab = rose;
            SpawnBall();

        } else if (ScentOptionScript.vanillaSelected)
        {
            prefab = vanilla;
            SpawnBall();

        } else if (ScentOptionScript.coffeeSelected)
        {
            prefab = coffee;
            SpawnBall();

        } else if (ScentOptionScript.garlicSelected)
        {
            prefab = garlic;
            SpawnBall();

        } else if (ScentOptionScript.orangeSelected)
        {
            prefab = orange;
            SpawnBall();

        } else if (ScentOptionScript.soapSelected)
        {
            prefab = soap;
            SpawnBall();

        }
    }
}
