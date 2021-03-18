using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASLogics : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public bool[] spawnPointOccupied;

    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;
    public int usedAmount;

    bool overpopulated;


    void Start()
    {
        spawnPointOccupied = new bool[6];
        overpopulated = false;
        usedAmount = 0;

        for (int i = 0; i < spawnPointOccupied.Length; i++)
        {
            spawnPointOccupied[i] = false;
        }
        Play();
        // InvokeRepeating("GoTime", 1f, 1f);
    }

    public void Play()
    {
        StartCoroutine("GameSequence");
    }

    IEnumerator GameSequence()
    {   
        while(usedAmount < 2)
        {
            //First 12 seconds, there should spawn 1 every 3 sec
            GoTime();

            yield return new WaitForSeconds(1f);

            GoTime();

            yield return new WaitForSeconds(1f);

            GoTime();

            yield return new WaitForSeconds(1f);

            GoTime();


            yield break;


            //Between 12-30seconds, there should spawn 1 every 2 seconds

            //Between 30-60 seconds there shoul spawn every one second.

            //Max three on the playfield at once.
        }
    }

    // void HowMany()
    // {
    //     int usedAmount = 0;


    //     for (int j = 0; j < spawnPointOccupied.Length; j++)
    //     {
    //         if(spawnPointOccupied[j] == false)
    //         {
    //             usedAmount++;
    //             if(usedAmount >= 3)
    //             {
    //                 overpopulated = true;
    //             }
    //         }
    //     }
    // }

    public void GoTime()
    {
        int i = Random.Range(0, 5);

        Debug.Log(i + " was chosen.");

        // HowMany();

        if(spawnPointOccupied[i] == false)
        {
            GameObject child = Instantiate(prefab, spawnPoints[i].transform.position, Quaternion.identity);
            child.transform.SetParent(spawnPoints[i].transform);
            child.GetComponent<WASObjectLogics>().myNumber = i;
            usedAmount++;
            Debug.Log(usedAmount);

            spawnPointOccupied[i] = true;
        } else 
        {
            GoTime();
        }
    }
}
