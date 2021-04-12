using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperLogics : MonoBehaviour
{
    public UILogics UI;
    public Timer Timer;
    public Score ScoreCounter;
    public Countdown CountdownLogics;
    public JumperPlayer PlayerLogics;

    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject clove;
    public GameObject orange;


    public GameObject obstacles;
    public GameObject lowSpawn;
    public GameObject mediumSpawn;
    public GameObject highSpawn;

    public bool gamePlaying;
    bool gameOver;
    bool obstacleHitBool;

    public float prefabSpeed;
    public float minTime;
    public float maxTime;

    public int score;
    public int timeRemaining;


    void Start()
    {
        gamePlaying = false;
        score = 0;
        timeRemaining = 60;
        obstacleHitBool = false;
    }

    public void selectGarlic()
    {
        prefab = garlic;
        UI.StartCoroutine("SelectedScent", "Garlic");
    }

    public void selectApple()
    {
        prefab = apple;
        UI.StartCoroutine("SelectedScent", "Apple");
    }
    
    public void selectSoap()
    {
        prefab = soap;
        UI.StartCoroutine("SelectedScent", "Soap");
    }

    public void selectCoffee()
    {
        prefab = coffee;
        UI.StartCoroutine("SelectedScent", "Coffee");
    }

    public void selectClove()
    {
        prefab = clove;
        UI.StartCoroutine("SelectedScent", "Clove");
    }

    public void selectOrange()
    {
        prefab = orange;
        UI.StartCoroutine("SelectedScent", "Orange");
    }

    public void Play()
    {   
        if(prefab != null && gamePlaying == false)
        {
            gamePlaying = true;
            prefabSpeed = -9f;
            minTime = 0.7f;
            maxTime = 1.1f;
            // StartCoroutine("GameSequence");
            Debug.Log("Game is now playing.");
            // Timer.ToggleTimer();
            StartCoroutine("PlayCoroutine");
        }
    }

    IEnumerator PlayCoroutine()
    {
        CountdownLogics.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds(2);

        PlayerLogics.paused = false;
        StartCoroutine("GameSequence");
        StartCoroutine("ToggleTimer");
        yield break;
    }

    void GameOver()
    {
        prefabSpeed = 0f;
        StopCoroutine("RandomSpawn");
        FreezeObstacles();
        PlayerLogics.FreezePlayer();
        UI.AnouncementText("Game Over");
        if(obstacleHitBool)
        {
            StopCoroutine("ObstacleHit");
            CountdownLogics.StopCoroutine("TwoSeconds");
        }
    }

    void FreezeObstacles()
    {
        foreach(Transform child in obstacles.transform)
        {
            // child.GetComponent<Rigidbody2D>().velocity  = new Vector2(0,0);
            // child.transform.Translate(0, 0, 0);
            child.GetComponent<JumperPrefabLogics>().frozen = true;
        }
    }

    void UnfreezeObstacles()
    {
        foreach(Transform child in obstacles.transform)
        {
            // child.GetComponent<Rigidbody2D>().velocity  = new Vector2(prefabSpeed,0);
            // child.transform.Translate(prefabSpeed * Time.deltaTime, 0, 0);
            child.GetComponent<JumperPrefabLogics>().frozen = false;
        }
    }

    IEnumerator ObstacleHit()
    {
        FreezeObstacles();
        UI.AnouncementText("PLAYER HIT!");
        CountdownLogics.StartCoroutine("TwoSeconds");
        PlayerLogics.FreezePlayer();
        StopCoroutine("RandomSpawn");
        obstacleHitBool = true;

        yield return new WaitForSeconds(2);

        UI.AnouncementText("");
        UnfreezeObstacles();
        PlayerLogics.UnfreezePlayer();
        StartCoroutine("RandomSpawn");
        obstacleHitBool = false;

        yield break;
    }
    



    public void AddScore()
    {
        score++;
        ScoreCounter.AddScore();


    }

    IEnumerator GameSequence()
    {
        SpawnLow();
                
        yield return new WaitForSeconds(1f);

        SpawnLow();

        StartCoroutine("RandomSpawn");

    }

    IEnumerator RandomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

                int i = Random.Range(1, 11);

                if (i >= 1 && i < 5)
                {
                    SpawnLow();
                } else if(i >= 5 && i < 9)
                {
                    SpawnMedium();
                } else if (i >= 9 && i <= 10)
                {
                    SpawnHigh();
                }
        }
    }

    void SpeedUp()
    {
        prefabSpeed -= 0.05f;
        
        //ADD IF STATEMENT TO STOP MAKING IT SPAWN FASTER AFTER A CERTAIN CAP HAS BEEN HIT, IF NOT THE GAME EVENTUALLY CRASHES
        if(minTime >= 0.5f)
        {
            minTime -= 0.01f;
            maxTime -= 0.01f;
        }
    }

    public void SpawnLow()
    {
        GameObject childObject = Instantiate(prefab, new Vector3(lowSpawn.transform.position.x, lowSpawn.transform.position.y,0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        SpeedUp();
    }

    public void SpawnMedium()
    {
        GameObject childObject = Instantiate(prefab, new Vector3(mediumSpawn.transform.position.x, mediumSpawn.transform.position.y,0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        SpeedUp();
    }

    public void SpawnHigh()
    {
        GameObject childObject = Instantiate(prefab, new Vector3(highSpawn.transform.position.x, highSpawn.transform.position.y,0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        SpeedUp();
    }   


                            //COPY PASTED FUNCTIONS THAT IS SPLITTED AMONG ALL
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
                GameOver();
                yield break;
            }
        }
    } 
}
