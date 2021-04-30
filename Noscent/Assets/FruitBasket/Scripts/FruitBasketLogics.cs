using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitBasketLogics : MonoBehaviour
{
    public UILogics UI;
    public Timer Timer;
    public Score ScoreCounter;
    public Countdown CountdownLogics;
    public FBPlayerLogics PlayerLogics;
    public ScentOptionScript ScentOptionScript;

    public GameObject playPanel;
    public GameObject obstacles;
    public GameObject prefab;
    public GameObject apple;
    public GameObject garlic;
    public GameObject soap;
    public GameObject coffee;
    public GameObject clove;
    public GameObject orange;

    public GameObject player;
    public float movementSpeed;

    public int life;
    public int scoreGoal;
    public int score;
    public int time;
    public int timeRemaining;

    // public float timeRemaining;

    bool counting; 

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        counting = false;
        timeRemaining = 60;

    }



    public void Play()
    {   
        if(prefab != null)
        {
            // Timer.ToggleTimer();
            // StartCoroutine("GameSequence");
            playPanel.SetActive(false);
            StartCoroutine("PlayCoroutine");
        }
    }

    IEnumerator PlayCoroutine()
    {
        UI.StartCoroutine("TwoSeconds");
        PlayerLogics.movementSpeed = movementSpeed;
        // Cursor.visible = false;

        yield return new WaitForSeconds(2);

        PlayerLogics.paused = false;
        StartCoroutine("GameSequence");
        StartCoroutine("ToggleTimer");
    }

    public void GameOver()
    {
        FreezeObstacles();
        PlayerLogics.FreezePlayer();
        StopCoroutine("GameSequence");
        UI.AnouncementText("Game Over");
    }

    void FreezeObstacles()
    {
        foreach(Transform child in obstacles.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity  = new Vector2(0,0);
            child.GetComponent<Rigidbody2D>().isKinematic = true;

        }
    }

    void EasyDifficulty()
    {
        player.transform.localScale = new Vector3(150, player.transform.localScale.y, 0);
        movementSpeed = 15f;
    }

    void MediumDifficulty()
    {
        player.transform.localScale = new Vector3(125, player.transform.localScale.y, 0);
        movementSpeed = 12.5f;
    }

    void HardDifficulty()
    {
        player.transform.localScale = new Vector3(100, player.transform.localScale.y, 0);
        movementSpeed = 10f;
    }



    IEnumerator GameSequence()
    {
        while (true)
        {
            SpawnObject();

            yield return new WaitForSeconds(0.8f);
        }
    }

    public void Goal()
    {
        score++;
        UI.UpdateScore(score);

    }
    
    public void SpawnObject()
    {
        GameObject childObject = Instantiate(prefab, new Vector3(Random.Range(-10f, 10f), 5.20f, 0), Quaternion.identity);
        childObject.transform.parent = obstacles.transform;
        //Sets random speeed "fall speed" for the objects. Making them a bit unreliable
        childObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(-1f,-5f));
    }

    //ALLL FUNCTIONS THAT SHOULD BE UNIVERSAL GOES HERE. COULD POSSIBLY MAKE A NEW SCRRIPT LATER THAT HOSTS ALL THESE FUNCTIONS
    //INSTEAD OF HAVING THEM DUPLICATED IN EVERY MINI-GAMES LOGIC SCRIPT. WORTH CONSIDERING AS IT COULD SAVE TIME


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
                break;
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

    // public void Stop()
    // {

    // }

    // public void Pause()
    // {

    // }

    // public void Resume()
    // {

    // }

    // public void GameOver()
    // {

    // }

}
