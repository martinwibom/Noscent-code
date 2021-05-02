using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JumperLogics : MonoBehaviour
{
    public UILogics UI;
    public JumperPlayer PlayerLogics;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;

    public GameObject scorePrefab;
    public GameObject wind;
    public GameObject prefab;
    public Sprite apple;
    public Sprite garlic;
    public Sprite soap;
    public Sprite coffee;
    public Sprite clove;
    public Sprite orange;
    
    public SpriteRenderer jarPic;

    public List<AudioSource> snifflesList = new List<AudioSource>();

    public GameObject jar;
    public GameObject nose;
    public GameObject clouds;
    public GameObject obstacles;
    public GameObject smokePlayer;
    // public GameObject lowSpawn;
    // public GameObject mediumSpawn;
    // public GameObject highSpawn;
    
    public List<GameObject> leftSpawnPoints = new List<GameObject>();
    public List<GameObject> rightSpawnPoints = new List<GameObject>();
    public List<GameObject> scoreSpawnPoints = new List<GameObject>();
    List<GameObject> tempSpawnPoints;
    public GameObject[] beams = new GameObject[3];
    // public GameObject scoreMultiText;

    public bool gamePlaying;
    bool gameOver;
    bool obstacleHitBool;
    public bool playerInBeam;
    
    [HideInInspector]
    public bool[] playerPosition = new bool[3];

    [HideInInspector]
    public bool[] powerBeam = new bool[3];

    public float prefabSpeed;
    public float minTime;
    public float maxTime;

    public int life;
    public int scoreGoal;
    public int score;
    public int timeRemaining;
    int sniffleNum = 0;

    Transform scoreSpawnPoint;


    void Start()
    {
        gamePlaying = false;
        score = 0;
        timeRemaining = 60;
        obstacleHitBool = false;
        UI.UpdateScoreJumper(score);
    }

    public void Play()
    {   
        if(prefab != null && gamePlaying == false)
        {
            gamePlaying = true;
            Debug.Log("Game is now playing.");
            PlayPanelScript.gameObject.SetActive(false);
            // SpawnPlayer();
            StartCoroutine("PlayCoroutine");
        }
    }

    void Update() {
        CheckPosition();
        // if(playerInBeam && !scoreMultiText.activeSelf) scoreMultiText.SetActive(true);
        // if(!playerInBeam && scoreMultiText.activeSelf) scoreMultiText.SetActive(false);
    }

    IEnumerator PlayCoroutine()
    {
        UI.StartCoroutine("TwoSeconds");
        // UI.SetLife(life);
        prefab = wind;

        yield return new WaitForSeconds(3);

        PlayerLogics.StartCoroutine("PlayerSmelling");
        MoveClouds();
        PlayerLogics.paused = false;
        StartCoroutine("GameSequence");
        // StartCoroutine("ToggleTimer");

        yield return new WaitForSeconds(5f);

        StartCoroutine("ChangeBeam");

        yield break;
    }

    void GameOver(string text)
    {
        StopClouds();
        prefabSpeed = 0f;
        StopCoroutine("ChangeBeam");
        PlayerLogics.StopAllCoroutines();
        StopCoroutine("RandomObstacleSpawn");
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
        prefabSpeed = -4.5f;
        minTime = 1.4f;
        maxTime = 1.9f;
        life = 5;
        scoreGoal = 15;
    }

    void MediumDifficulty()
    {
        prefabSpeed = -5.3f;
        minTime = 1.2f;
        maxTime = 1.5f;
        life = 5;
        scoreGoal = 15;
    }

    void HardDifficulty()
    {
        prefabSpeed = -7f;
        minTime = 0.9f;
        maxTime = 1.0f;
        life = 5;
        scoreGoal = 15;
    }

    
    void LifeLost()
    {
        life--;
        UI.LoseLife();
        if(life == 0) GameOver("Game over");
    }


    public void UpdatePlayerPosition(int lane)
    {
        for (int i = 0; i < playerPosition.Length; i++) playerPosition[i] = false;
        playerPosition[lane] = true;
        CheckPosition();
    }


    void CheckPosition()
    {
        for (int i = 0; i <  playerPosition.Length; i++)
        {
            if(playerPosition[i] && powerBeam[i])
            {
                playerInBeam = true;
                PlayerLogics.smelling = true;
            }
        } 
    }


    IEnumerator GameSequence()
    {
        // SpawnLow();
        SpawnObstacle(rightSpawnPoints[0], false);
                
        yield return new WaitForSeconds(1f);

        SpawnObstacle(rightSpawnPoints[1], false);
        // SpawnLow();

        StartCoroutine("RandomObstacleSpawn");
        // StartCoroutine("RandomScoreSpawn");
    }

                //PLAYER LOGICS
    
    // public void SpawnPlayer()
    // {
    //     // GameObject player =  Instantiate(smokePlayer, new Vector3(0, -2.77f, 0), Quaternion.identity);
    //     GameObject player =  Instantiate(smokePlayer, new Vector3(0, -2.529f, 0), Quaternion.identity);
    //     player.name = "Player";
    //     PlayerLogics = player.GetComponent<JumperPlayer>();
    //     PlayerLogics.Logics = this;
    //     PlayerLogics.nose = nose;
    // }


                //SCORE LOGICS
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
            PlaySniffle();
        }
    }
    
    public void AddScore()
    {
        score++;
        // if(playerInBeam) score++;
        UI.UpdateScoreJumper(score);
        if(score >= scoreGoal) GameOver("You won!");
    }


                    //OBSTACLE LOGICS

    // IEnumerator RandomObstacleSpawn()
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

    void FreezeObstacles()
    {
        foreach(Transform child in obstacles.transform) child.GetComponent<JumperPrefabLogics>().frozen = true;
    }

    void UnfreezeObstacles()
    {
        foreach(Transform child in obstacles.transform) child.GetComponent<JumperPrefabLogics>().frozen = false;
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
        StopCoroutine("RandomObstacleSpawn");
        obstacleHitBool = true;

        yield return new WaitForSeconds(2);

        UI.AnouncementText("");
        UnfreezeObstacles();
        MoveClouds();
        PlayerLogics.UnfreezePlayer();
        StartCoroutine("RandomObstacleSpawn");
        obstacleHitBool = false;

        yield break;
    }

    IEnumerator ObstacleHitNew()
    {
        LifeLost();
        if(life == 0) yield break;

        obstacleHitBool = true;
        UI.AnouncementText("Scent blown away!");
        // PlayerLogics.smelling= false;
        PlayerLogics.StopCoroutine("PlayerSmelling");
        var PlayerSprite =  PlayerLogics.gameObject.GetComponent<SpriteRenderer>();
        PlayerLogics.gameObject.tag ="Untagged";
        PlayerSprite.enabled = false;

        yield return new WaitForSeconds(0.5f);
        PlayerSprite.enabled = true;

        yield return new WaitForSeconds(0.5f);
        PlayerSprite.enabled = false;

        yield return new WaitForSeconds(0.5f);
        PlayerSprite.enabled = true;

        yield return new WaitForSeconds(0.5f);
        PlayerSprite.enabled = false;

        yield return new WaitForSeconds(0.5f);
        PlayerSprite.enabled = true;

        UI.AnouncementText("");
        // PlayerLogics.smelling = true;
        PlayerLogics.StartCoroutine("PlayerSmelling");
        PlayerLogics.gameObject.tag ="Player";
        obstacleHitBool = false;

        yield break;
    }

    IEnumerator RandomObstacleSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            int side = Random.Range(1,3);
            int spawnheight = Random.Range(0,3);
            if(side == 1)
            {  
                SpawnObstacle(leftSpawnPoints[spawnheight], true);
            } else
            {
                SpawnObstacle(rightSpawnPoints[spawnheight], false);
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

    void SpawnObstacle(GameObject spawnPoint, bool movingRight)
    {
        GameObject childObject = Instantiate(prefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y,0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        childObject.GetComponent<JumperPrefabLogics>().movingRight = movingRight;
        if(movingRight) childObject.GetComponent<SpriteRenderer>().flipX = true;
        SpeedUp();
    }

    // public void SpawnLow()
    // {
    //     GameObject childObject = Instantiate(prefab, new Vector3(lowSpawn.transform.position.x, lowSpawn.transform.position.y,0), Quaternion.identity);
    //     childObject.transform.parent = obstacles.transform;
    //     SpeedUp();
    // }

    // public void SpawnMedium()
    // {
    //     GameObject childObject = Instantiate(prefab, new Vector3(mediumSpawn.transform.position.x, mediumSpawn.transform.position.y,0), Quaternion.identity);
    //     childObject.transform.parent = obstacles.transform;
    //     SpeedUp();
    // }

    // public void SpawnHigh()
    // {
    //     GameObject childObject = Instantiate(prefab, new Vector3(highSpawn.transform.position.x, highSpawn.transform.position.y,0), Quaternion.identity);
    //     childObject.transform.parent = obstacles.transform;
    //     SpeedUp();
    // }   

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
                    jar.transform.position = new Vector3(scoreSpawnPoints[i].transform.position.x, jar.transform.position.y, jar.transform.position.z);
                    powerBeam[i] = true;
                    CheckPosition();
                } else
                {
                    beams[i].GetComponent<JumperBeamLogics>().ChangeBeam(false); 
                    powerBeam[i] = false;        
                }
            }

            yield return new WaitForSeconds(10f);
        }   
    }

                            //RANDOM FUNCTIONS
    
    void MoveClouds()
    {
        clouds.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.04f, 0f);
    }
    void StopClouds()
    {
        clouds.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }

    void PlaySniffle()
    {
        snifflesList[sniffleNum].Play();
        sniffleNum++;
        if(sniffleNum == snifflesList.Count) sniffleNum = 0;          
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
            // prefab = apple;
            jarPic.sprite = apple;
            jarPic.transform.localScale = new Vector3(3.5f, 3.5f, 1);
        } else if (ScentOptionScript.cloveSelected)
        {
            jarPic.sprite = clove;
            jarPic.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        } else if (ScentOptionScript.coffeeSelected)
        {
            jarPic.sprite = coffee;
            jarPic.transform.localScale = new Vector3(3.0f, 3.0f, 1);
        } else if (ScentOptionScript.garlicSelected)
        {
            jarPic.sprite = garlic;
            jarPic.transform.localScale = new Vector3(2.0f, 2.0f, 1);
        } else if (ScentOptionScript.orangeSelected)
        {
            jarPic.sprite = orange;
            jarPic.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        } else if (ScentOptionScript.soapSelected)
        {
            jarPic.sprite = soap;
            jarPic.transform.localScale = new Vector3(1.8f, 1.8f, 1);
        }
        prefab = wind;
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
