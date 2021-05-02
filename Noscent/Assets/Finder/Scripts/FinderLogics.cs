using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class FinderLogics : MonoBehaviour
{
    public UILogics UI;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;

    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject rightWall;
    public GameObject leftWall;

    public AudioSource startSmelling;
    public AudioSource stopSmelling;

    public GameObject objects;
    public Sprite prefab;
    public Sprite apple;
    public Sprite garlic;
    public Sprite soap;
    public Sprite coffee;
    public Sprite clove;
    public Sprite orange;

    public List<Sprite> coffeBgList = new List<Sprite>();
    public List<Sprite> appleBgList = new List<Sprite>();
    public List<Sprite> cloveBgList = new List<Sprite>();
    public List<Sprite> soapBgList = new List<Sprite>();
    public List<Sprite> orangeBgList = new List<Sprite>();
    public List<Sprite> garlicBgList = new List<Sprite>();
    public List<Sprite> gameBgList;

    public SpriteRenderer background;
    public Transform blackBG;

    public int timeRemaining;
    public int score;
    public int streak;
    public int numberOfCorrect;
    public int maxCorrect;
    public int round;
    public int lifes;
    int BGnumber;
    
    public float smellTime;
    public float clickTime;

    public List<GameObject> objectList = new List<GameObject>();
    List<GameObject> tempList;
    

    void Start()
    {
        tempList = new List<GameObject>(objectList);
        score = 0;
        round = 0;
        
    }

    void RandomCorrect(int maxRange)
    {
        int i = Random.Range(0, maxRange);
        var randomObject = tempList[i].GetComponent<PrefabFinderLogics>();
        Debug.Log(randomObject.name);
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
        BGnumber=0;
        lifes = 3;
        StartCoroutine("GameSequence");
        // StartCoroutine("ToggleTimer");
        ActivateWalls();
    }

    IEnumerator GameSequence()
    {
        while(true){
            
            // background.color = new Color(1,1,1,1);
            // background.DOFade(0,smellTime);
            // GreenWalls();

            NewRound();

            yield return new WaitForSeconds(smellTime);

            stopSmelling.Play();
            blackBG.DOMove(new Vector3(0,11.81f,0), clickTime, false);
            AllClickable();
            AllAnon();
            // RedWalls();

            yield return new WaitForSeconds(clickTime);
        }
    }

    void NewRound()
    {
        if(round == 5)
        {
            GameOver();
            return;
        }
        round++;
        UI.UpdateRound(round);
        ChangeBackground();
        blackBG.position = new Vector3(0,11.81f,0);
        blackBG.DOMove(new Vector3(0,1.72f,0), smellTime, false);
        startSmelling.Play();
        AllUnclickable();
        // CheckCorrect();
        SelectCorrect(maxCorrect);
    }


    void ChangeBackground()
    {
            background.sprite = gameBgList[BGnumber];
            BGnumber++;
            if(BGnumber== gameBgList.Count) BGnumber = 0;
    }

    void MoveBG()
    {
        blackBG.DOMove(new Vector3(0,1,0), smellTime, false);
    }

    public void LoseLife()
    {
        lifes--;
        UI.LoseLife();
        if(lifes == 0)
        {
            GameOver();
            return;
        }

    }

    void EasyDifficulty()
    {
        maxCorrect = 2;
        smellTime = 7f;
        clickTime = 3f;
    }

    void MediumDifficulty()
    {
        maxCorrect = 4;
        smellTime = 8f;
        clickTime = 4f;
    }

    void HardDifficulty()
    {
        maxCorrect = 6;
        smellTime = 9f;
        clickTime = 5f;
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

    // public void CheckCorrect()
    // {
    //     if(numberOfCorrect == maxCorrect)
    //     {
    //         streak++;
    //     } else
    //     {
    //         streak = 0;
    //     }
    //     numberOfCorrect = 0;
    // }

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

    void ActivateWalls()
    {
       leftWall.SetActive(true);
       rightWall.SetActive(true);
       bottomWall.SetActive(true);
       topWall.SetActive(true); 
    }

    void GreenWalls()
    {
        leftWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
        rightWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
        bottomWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
        topWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
    }

    void RedWalls()
    {
        leftWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
        rightWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
        bottomWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
        topWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
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
            gameBgList = new List<Sprite>(appleBgList);
        } else if (ScentOptionScript.cloveSelected)
        {
            prefab = clove;
            gameBgList = new List<Sprite>(cloveBgList);
        } else if (ScentOptionScript.coffeeSelected)
        {
            prefab = coffee;
            gameBgList = new List<Sprite>(coffeBgList);
        } else if (ScentOptionScript.garlicSelected)
        {
            prefab = garlic;
            gameBgList = new List<Sprite>(garlicBgList);
        } else if (ScentOptionScript.orangeSelected)
        {
            prefab = orange;
            gameBgList = new List<Sprite>(orangeBgList);
        } else if (ScentOptionScript.soapSelected)
        {
            prefab = soap;
            gameBgList = new List<Sprite>(soapBgList);
        }
    }

    public void CheckDifficulty()
    {
        if(ScentOptionScript.easySelected) EasyDifficulty();
        if (ScentOptionScript.mediuSelected) MediumDifficulty();
        if (ScentOptionScript.hardSelected) HardDifficulty();
    }

}
