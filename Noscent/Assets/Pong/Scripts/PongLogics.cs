using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongLogics : MonoBehaviour
{
    public BallLogics BallLogics;
    public PlayerLogics PlayerLogics;

    public GameObject player;
    public GameObject ball;
    public GameObject goal;
    public GameObject goalCounter;

    public int goalCount;

    public bool paused = true;
    public bool gamePlaying = false;

    Vector3 ballStartPosition;
    Vector3 playerStartPosition;


    // Start is called before the first frame update
    void Start()
    {
        ballStartPosition = ball.transform.position;
        playerStartPosition = player.transform.position;
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Play()
    {   
        if(!gamePlaying)
        {
            PlayerLogics.paused = false;
            BallLogics.Launch();
            goalCount = 0;
            goalCounter.GetComponent<TextMeshProUGUI>().text = goalCount.ToString();
            gamePlaying = true;
            paused = false;
        }
    }
    
    public void Pause()
    {
        BallLogics.Pause();
        PlayerLogics.paused = true;
    }

    public void Resume()
    {
        if(paused && gamePlaying)
        {
            BallLogics.Resume();
            PlayerLogics.paused = false;
        }
    }

    public void Restart()
    {
        ball.transform.position = ballStartPosition;
        player.transform.position = playerStartPosition;

        BallLogics.Launch();
    }

    public void Goal()
    {
        goalCount++;
        goalCounter.GetComponent<TextMeshProUGUI>().text = goalCount.ToString();

    }

    public void Miss()
    {
        goalCount--;
        goalCounter.GetComponent<TextMeshProUGUI>().text = goalCount.ToString();

    }

}
