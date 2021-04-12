using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderLogics : MonoBehaviour
{
    public UILogics UI;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;
    
    public GameObject objects;
    public Sprite prefab;
    public Sprite apple;
    public Sprite garlic;
    public Sprite soap;
    public Sprite coffee;
    public Sprite clove;
    public Sprite orange;

    public int timeRemaining;
    public int score;
    public int streak;
    public int numberOfCorrect;
    public int maxCorrect;
    
    public float smellTime;
    public float clickTime;
    

    public List<GameObject> objectList = new List<GameObject>();
    List<GameObject> tempList;
    

    void Start()
    {
        tempList = new List<GameObject>(objectList);
        // prefab = apple;
        score = 0;
        // Play();
    }

    void RandomCorrect(int maxRange)
    {
        int i = Random.Range(0, maxRange);
        var randomObject = tempList[i].GetComponent<PrefabFinderLogics>();
        randomObject.correctPrefab = true;
        randomObject.PrefabSprite(prefab);
        randomObject.prefab = prefab;
        tempList.RemoveAt(i);
    }

    public void Play()
    {
        PlayPanelScript.gameObject.SetActive(false);
        StartCoroutine("PlayCoroutine");

    }

    IEnumerator PlayCoroutine()
    {
        UI.StartCoroutine("TwoSeconds");

        yield return new WaitForSeconds (3);

        timeRemaining = 60;
        streak = 1;
        StartCoroutine("GameSequence");
        StartCoroutine("ToggleTimer");
    }

    IEnumerator GameSequence()
    {
        while(true){
            AllUnclickable();
            // ThreeCorrect();
            CheckCorrect();
            SelectCorrect(streak);

            yield return new WaitForSeconds(smellTime);

            AllClickable();
            AllAnon();


            yield return new WaitForSeconds(clickTime);
        }
    }






    public void GameOver()
    {
        StopCoroutine("GameSequence");
        AllUnclickable();
        UI.AnouncementText("Game over!");
    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        UI.UpdateScore(score);
    }

    void AllBlank()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].GetComponent<PrefabFinderLogics>().BlankSprite();
            objectList[i].GetComponent<PrefabFinderLogics>().correctPrefab = false;
        }
    }

    void AllAnon()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].GetComponent<PrefabFinderLogics>().BlankSprite();
        }
    }

    void AllClickable()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].GetComponent<PrefabFinderLogics>().clickable = true;
        }
    }

    void AllUnclickable()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].GetComponent<PrefabFinderLogics>().clickable = false;
        }
    }

    public void CheckCorrect()
    {
        if(numberOfCorrect == maxCorrect)
        {
            streak++;
        } else
        {
            streak = 2;
        }
        numberOfCorrect = 0;
        maxCorrect = streak;
    }

    void ThreeCorrect()
    {
        tempList = new List<GameObject>(objectList);
        AllBlank();
        RandomCorrect(objectList.Count);
        RandomCorrect(objectList.Count -1);
        RandomCorrect(objectList.Count -2);
    }

    void SelectCorrect(int amountCorrect)
    {
        tempList = new List<GameObject>(objectList);
        var objectListCount = objectList.Count;
        AllBlank();
        for (int i = 0; i < amountCorrect; i++)
        {
            RandomCorrect(objectListCount);
            objectListCount--;
        }
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
                GameOver();
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






//      !!!!!!!!!!!!!THIS CODE RUNS ON A VECTOR3 BASED SYSTEM. OLD AND MIGHT NOT BE USED!!!!!!!!!!!!!!!!!!!!
//    public List<GameObject> spawnPoints = new List<GameObject>();
//     List<GameObject> tempList = new List<GameObject>();
//     List<GameObject> hiddenObjects = new List<GameObject>(); 
    // IEnumerator PlaySession()
    // {
    //     SpawnObjects();
        
    //     yield return new WaitForSeconds(5);

    //     RemoveObjects();

    //     SpawnAnonObjects();


    // }

    // void RandomSpawn(int maxRange)
    // {
    //     int i = Random.Range(0, maxRange);

    //     GameObject childObject = Instantiate(apple, tempList[i].transform.position, Quaternion.identity);
    //     childObject.transform.SetParent(objects.transform);
    //     hiddenObjects.Add(tempList[i]);
    //     tempList.RemoveAt(i);
    // }

    // void SpawnAnonObjects()
    // {
    //     for (int i = 0; i < spawnPoints.Count; i++)
    //     {
    //         if(hiddenObjects.Contains(spawnPoints[i]))
    //         {
    //             Debug.Log("Correct objected should have spawned at " + spawnPoints[i]);
    //             GameObject child = Instantiate(correctObject, spawnPoints[i].transform.position, Quaternion.identity);
    //             child.transform.SetParent(objects.transform);
    //         } else
    //         {
    //             GameObject child = Instantiate(wrongObject, spawnPoints[i].transform.position, Quaternion.identity);
    //             child.transform.SetParent(objects.transform);
    //         }
    //     }
    // }

    // void RemoveObjects()
    // {
    //     foreach (Transform child in objects.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }
    
    // void SpawnObjects()
    // {
    //     hiddenObjects.Clear();
    //     tempList = spawnPoints;
    //     RandomSpawn(9);
    //     RandomSpawn(8);
    //     RandomSpawn(7);
    // }

    // void Start()
    // {
    //     // SpawnObjects();
    //     // StartCoroutine("PlaySession");
    // }


}
