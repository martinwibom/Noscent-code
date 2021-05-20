using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgerLogics : MonoBehaviour
{
    public UILogics UI;
    public Timer Timer;
    public Score ScoreCounter;
    public Countdown CountdownLogics;
    public PlayerDodgerLogics PlayerLogics;
    public ScentOptionScript ScentOptionScript;
    

    Vector3 middleLane = new Vector3(0, 5.5f,0);
    Vector3 rightLane = new Vector3(2, 5.5f, 0);
    Vector3 leftLane = new Vector3(-2, 5.5f, 0);

    public GameObject playPanel;
    public GameObject obstacles;
    public GameObject prefab;
    public GameObject rose;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject vanilla;
    public GameObject orange;

    public GameObject goal;

    public int time;
    public int gameTime;
    


    void Start()
    {
        time = 0;
        gameTime = 0;
        UI.UpdateTime(0);
    }

    public void Play()
    {
        if(prefab != null)
        {
            playPanel.SetActive(false);    
            StartCoroutine("PlayCoroutine");
        }
    }

    public IEnumerator PlayerHit()
    {
        // StopCoroutine("RandomSpawn");
        FreezeObjects();
        PlayerLogics.FreezePlayer();
        UI.AnnouncementText("CRASHED");
        UI.StartCoroutine("TwoSeconds");
        StopCoroutine("GameStopwatch");
        StopCoroutine("RandomSpawn");

        yield return new WaitForSeconds(2);

        // StartCoroutine("RandomSpawn");
        StartCoroutine("GameStopwatch");
        StartCoroutine("RandomSpawn");
        UnfreezeObjects();
        UI.AnnouncementText("");
        UI.StopCoroutine("TwoSeconds");
        UI.cdText.text = "";
        PlayerLogics.UnfreezePlayer();
        
    }

    IEnumerator PlayCoroutine()
    {
        UI.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds(2);

            PlayerLogics.UnfreezePlayer();
            StartCoroutine("Stopwatch");    
            StartCoroutine("RandomSpawn");
            StartCoroutine("GameStopwatch");


    }

    public void GameOver()
    {
        FreezeObjects();
        StopCoroutine("Stopwatch");
        StopCoroutine("RandomSpawn");
        PlayerLogics.FreezePlayer();
        UI.AnnouncementText("You won!");
    }


    public void CheckScent()
    {
        if(ScentOptionScript.roseSelected)
        {
            prefab = rose;
        } else if (ScentOptionScript.vanillaSelected)
        {
            prefab = vanilla;
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


    IEnumerator Stopwatch()
    {
        while(true)
        {
            time++;
            UI.UpdateTime(time);
            
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator GameStopwatch()
    {
        Debug.Log("Gametime is running");
        while(true)
        {
            gameTime++;
            
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator RandomSpawn()
    {
        Debug.Log("RandomSpawn is running");
        while(true)
        {
            if(gameTime % 3 == 0)
            {
                RandomSpawnObject();
                yield return new WaitForSeconds(2);
            }
            yield return null;

        }
        // RandomSpawnObject();
        // while(true)
        // {
        //     yield return new WaitForSeconds(3);
        //     RandomSpawnObject();

        // }
    }

    public void FreezeObjects()
    {
        foreach(Transform child in obstacles.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity  = new Vector2(0,0);
            child.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void UnfreezeObjects()
    {
        foreach(Transform child in obstacles.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity  = new Vector2(0,-3f);
            child.GetComponent<Rigidbody2D>().isKinematic = false;

        }
    }


    void RandomSpawnObject()
    {
        int i = Random.Range(1,4);
        Debug.Log(i);

        if(i == 1)
        {
            SpawnObjectLeft();
        } else if (i == 2)
        {
            SpawnObjectMiddle();
        } else if (i == 3)
        {
            SpawnObjectRight();
        }
    }

    public void SpawnObject(Vector3 lane)
    {
        GameObject childObject = Instantiate(prefab,lane, Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
    }


    void SpawnObjectLeft()
    {
        SpawnObject(middleLane);
        SpawnObject(rightLane);
        // Instantiate(rose,middleLane, Quaternion.identity);
        // Instantiate(rose,rightLane, Quaternion.identity);
    }
    void SpawnObjectMiddle()
    {
        SpawnObject(leftLane);
        SpawnObject(rightLane);
        // Instantiate(rose,leftLane, Quaternion.identity);
        // Instantiate(rose,rightLane, Quaternion.identity);
    }
    void SpawnObjectRight()
    {
        SpawnObject(leftLane);
        SpawnObject(middleLane);
        // Instantiate(rose,leftLane, Quaternion.identity);
        // Instantiate(rose,middleLane, Quaternion.identity);
    }


}
