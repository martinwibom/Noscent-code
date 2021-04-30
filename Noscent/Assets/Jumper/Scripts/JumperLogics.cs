using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperLogics : MonoBehaviour
{
    public UILogics UI;
    public JumperPlayer PlayerLogics;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;

    public GameObject scorePrefab;
    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject clove;
    public GameObject orange;

    public GameObject clouds;
    public GameObject obstacles;
    public GameObject lowSpawn;
    public GameObject mediumSpawn;
    public GameObject highSpawn;
    
    public List<GameObject> leftSpawnPoints = new List<GameObject>();
    public List<GameObject> rightSpawnPoints = new List<GameObject>();
    public List<GameObject> scoreSpawnPoints = new List<GameObject>();
    List<GameObject> tempSpawnPoints;
    public GameObject[] beams = new GameObject[3];
    public GameObject scoreMultiText;

    public bool gamePlaying;
    bool gameOver;
    bool obstacleHitBool;
    public bool pointMultiplier;

    public float prefabSpeed;
    public float minTime;
    public float maxTime;

    public int life;
    public int scoreGoal;
    public int score;
    public int timeRemaining;

    Transform scoreSpawnPoint;


    void Start()
    {
        gamePlaying = false;
        score = 0;
        timeRemaining = 60;
        obstacleHitBool = false;
    }

    public void Play()
    {   
        if(prefab != null && gamePlaying == false)
        {
            gamePlaying = true;
            // prefabSpeed = -7f;
            // minTime = 1f;
            // maxTime = 1.3f;
            Debug.Log("Game is now playing.");
            PlayPanelScript.gameObject.SetActive(false);
            StartCoroutine("PlayCoroutine");
        }
    }

    private void Update() {
        if(pointMultiplier && !scoreMultiText.activeSelf) scoreMultiText.SetActive(true);
        if(!pointMultiplier && scoreMultiText.activeSelf) scoreMultiText.SetActive(false);
    }

    IEnumerator PlayCoroutine()
    {
        UI.StartCoroutine("TwoSeconds");
        UI.SetLife(life);

        yield return new WaitForSeconds(2);
        StartCoroutine("ChangeBeam");
        MoveClouds();
        PlayerLogics.paused = false;
        StartCoroutine("GameSequence");
        // StartCoroutine("ToggleTimer");
        yield break;
    }

    void GameOver(string text)
    {
        StopClouds();
        prefabSpeed = 0f;
        StopCoroutine("RandomSpawn");
        StopCoroutine("RandomScoreSpawn");
        FreezeObstacles();
        PlayerLogics.FreezePlayer();
        UI.AnouncementText(text);
        if(obstacleHitBool)
        {
            StopCoroutine("ObstacleHit");
            UI.StopCoroutine("TwoSeconds");
        }
    }

    void EasyDifficulty()
    {
        prefabSpeed = -5f;
        minTime = 1.4f;
        maxTime = 1.9f;
        life = 5;
        scoreGoal = 20;
    }

    void MediumDifficulty()
    {
        prefabSpeed = -7f;
        minTime = 1f;
        maxTime = 1.3f;
        life = 4;
        scoreGoal = 20;
    }

    void HardDifficulty()
    {
        prefabSpeed = -9f;
        minTime = 0.6f;
        maxTime = 0.8f;
        life = 3;
        scoreGoal = 20;
    }

    void MoveClouds()
    {
        clouds.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.04f, 0f);
    }
    void StopClouds()
    {
        clouds.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
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
        LifeLost();
        if(life == 0) yield break;

        FreezeObstacles();
        StopClouds();
        UI.AnouncementText("PLAYER HIT!");
        UI.StartCoroutine("TwoSeconds");
        PlayerLogics.FreezePlayer();
        StopCoroutine("RandomSpawn");
        obstacleHitBool = true;

        yield return new WaitForSeconds(2);

        UI.AnouncementText("");
        UnfreezeObstacles();
        MoveClouds();
        PlayerLogics.UnfreezePlayer();
        StartCoroutine("RandomSpawn");
        obstacleHitBool = false;

        yield break;
    }
    
    void LifeLost()
    {
        life--;
        UI.LoseLife();
        if(life == 0) GameOver("Game over");
    }


    public void AddScore()
    {
        score++;
        if(pointMultiplier) score++;
        UI.UpdateScore(score);
        if(score >= scoreGoal) GameOver("You won!");


    }

    IEnumerator GameSequence()
    {
        SpawnLow();
                
        yield return new WaitForSeconds(1f);

        SpawnLow();

        StartCoroutine("RandomSpawn");
        StartCoroutine("RandomScoreSpawn");
    }

    void SpawnScore()
    {
        GameObject newScore = Instantiate(scorePrefab, scoreSpawnPoint.position, Quaternion.identity);
        Debug.Log("Score spawned");
    }

    IEnumerator RandomScoreSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            SpawnScore();
        }
    }

    // IEnumerator RandomSpawn()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(Random.Range(minTime, maxTime));
    //             int side = Random.Range(1,3);

    //             int i = Random.Range(1, 11);

    //             if (i >= 1 && i < 5)
    //             {
    //                 SpawnLow();
    //             } else if(i >= 5 && i < 9)
    //             {
    //                 SpawnMedium();
    //             } else if (i >= 9 && i <= 10)
    //             {
    //                 SpawnHigh();
    //             }
    //     }
    // }

    IEnumerator RandomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            int side = Random.Range(1,3);
            int spawnheight = Random.Range(0,3);
            if(side == 1)
            {  
                SpawnObject(leftSpawnPoints[spawnheight], true);
            } else
            {
                SpawnObject(rightSpawnPoints[spawnheight], false);
            }
        }
    }

    void SpeedUp()
    {
        prefabSpeed -= 0.05f;
        
        //ADD IF STATEMENT TO STOP MAKING IT SPAWN FASTER AFTER A CERTAIN CAP HAS BEEN HIT, IF NOT THE GAME EVENTUALLY CRASHES
        if(minTime >= 0.8f)
        {
            minTime -= 0.01f;
            maxTime -= 0.01f;
        }
    }

    void SpawnObject(GameObject spawnPoint, bool movingRight)
    {
        GameObject childObject = Instantiate(prefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y,0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        childObject.GetComponent<JumperPrefabLogics>().movingRight = movingRight;
        SpeedUp();
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

                            // BEAM LOGICS
    
    IEnumerator ChangeBeam()
    {
        while(true)
        {   
            int randomBeam = Random.Range(0,3);
            for (int i = 0; i < beams.Length; i++)
            {
                if(i == randomBeam)
                {
                    beams[i].GetComponent<JumperBeamLogics>().ChangeBeam(true);
                    scoreSpawnPoint = scoreSpawnPoints[i].transform;
                } else
                {
                    beams[i].GetComponent<JumperBeamLogics>().ChangeBeam(false);         
                }
            }

            yield return new WaitForSeconds(10f);
        }   
    }

                            //COPY PASTED FUNCTIONS THAT IS SPLITTED AMONG ALL
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
                GameOver("You won!");
                yield break;
            }
        }
    } 

    public void CheckScent()
    {
        if(ScentOptionScript.appleSelected)
        {
            prefab = apple;
        } else if (ScentOptionScript.cloveSelected)
        {
            prefab = clove;
        } else if (ScentOptionScript.coffeeSelected)
        {
            prefab = coffee;
        } else if (ScentOptionScript.garlicSelected)
        {
            prefab = garlic;
        } else if (ScentOptionScript.orangeSelected)
        {
            prefab = orange;
        } else if (ScentOptionScript.soapSelected)
        {
            prefab = soap;
        }
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
}
