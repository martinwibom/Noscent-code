using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperLogics : MonoBehaviour
{
    public Timer Timer;

    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;


    public GameObject obstacles;
    public GameObject lowSpawn;
    public GameObject mediumSpawn;
    public GameObject highSpawn;

    public bool gamePlaying;

    public float prefabSpeed;
    public float minTime;
    public float maxTime;


    void Start()
    {
        gamePlaying = false;
    }

    public void selectGarlic()
    {
        prefab = garlic;
    }

    public void selectApple()
    {
        prefab = apple;
    }

    public void Play()
    {   
        if(prefab != null && gamePlaying == false)
        {
            gamePlaying = true;
            prefabSpeed = -4f;
            minTime = 1f;
            maxTime = 1.6f;
            StartCoroutine("GameSequence");
            Debug.Log("Game is now playing.");
            Timer.ToggleTimer();
        }
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
        minTime -= 0.01f;
        maxTime -= 0.01f;
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
}
