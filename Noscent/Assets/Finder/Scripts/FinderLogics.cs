using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using TMPro;

public class FinderLogics : MonoBehaviour
{
    public UILogics UI;
    public ScentOptionScript ScentOptionScript;
    public PlayPanelScript PlayPanelScript;

    // public GameObject topWall;
    // public GameObject bottomWall;
    // public GameObject rightWall;
    // public GameObject leftWall;


    public AudioSource startSmelling;
    public AudioSource stopSmelling;

    public Sprite prefab;
    public Sprite rose;
    public Sprite garlic;
    public Sprite soap;
    public Sprite coffee;
    public Sprite vanilla;
    public Sprite orange;

    public List<Sprite> coffeBgList = new List<Sprite>();
    public List<Sprite> roseBgList = new List<Sprite>();
    public List<Sprite> vanillaBgList = new List<Sprite>();
    public List<Sprite> soapBgList = new List<Sprite>();
    public List<Sprite> orangeBgList = new List<Sprite>();
    public List<Sprite> garlicBgList = new List<Sprite>();
    List<Sprite> gameBgList;

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
    List<GameObject> correctList;
    

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
        // correctList.Add(tempList[i]);
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
        streak = 0;
        BGnumber=0;
        lifes = 3;
        StartCoroutine("GameSequence");
        // StartCoroutine("ToggleTimer");
        // ActivateWalls();
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
            UI.AnnouncementText("CLICK!");
            UI.anoText.color = Color.green;
            blackBG.DOMove(new Vector3(0,11.81f,0), clickTime, false);
            AllClickable();
            AllAnon();
            // RedWalls();

            yield return new WaitForSeconds(clickTime);
            CheckCorrect();
            UI.anoText.color = Color.green;
            // EndRound();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void NewRound()
    {
        if(round == 5)
        {
            GameOver(true);
            return;
        }
        round++;
        UI.UpdateRound(round);
        ChangeBackground();
        blackBG.position = new Vector3(0,11.81f,0);
        blackBG.DOMove(new Vector3(0,1.72f,0), smellTime, false);
        startSmelling.Play();
        AllUnclickable();
        SelectCorrect(maxCorrect);
    }

    // void EndRound()
    // {
    //     foreach (GameObject item in correctList)
    //     {
    //         var itemLogics = item.GetComponent<PrefabFinderLogics>();
    //         if(itemLogics.correctPrefab)
    //         {
    //             itemLogics.RedBG();
    //             itemLogics.PrefabSprite(prefab);
    //         }
    //     }
    // }


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
        UI.AnnouncementText("LIFE LOST!");
        if(lifes == 0)
        {
            GameOver(false);
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


    public void GameOver(bool won)
    {
        StopCoroutine("GameSequence");
        AllUnclickable();
        // UI.AnnouncementText(announcementText);
        if(won) UI.YouWon();
        else UI.GameOver();
    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        UI.UpdateScore(score);
    }

    // public void UpdateStreak(bool streakContinues)
    // {
    //     if(streakContinues)
    //     {
    //         streak++;
    //         streakText.text = streak.ToString();
    //     } else
    //     {
    //         streak = 0;
    //         streakText.text = streak.ToString();
    //     }
    // }

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
        if(numberOfCorrect != maxCorrect)
        {
            int missingCorrect = maxCorrect - numberOfCorrect;
            for (int i = 0; i < missingCorrect; i++)
            {
            LoseLife();
            }
        } 
        numberOfCorrect = 0;
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

    // void ActivateWalls()
    // {
    //    leftWall.SetActive(true);
    //    rightWall.SetActive(true);
    //    bottomWall.SetActive(true);
    //    topWall.SetActive(true); 
    // }

    // void GreenWalls()
    // {
    //     leftWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
    //     rightWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
    //     bottomWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
    //     topWall.GetComponent<SpriteRenderer>().color = new Color32(9,233,0,100);
    // }

    // void RedWalls()
    // {
    //     leftWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
    //     rightWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
    //     bottomWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
    //     topWall.GetComponent<SpriteRenderer>().color = new Color32(233,0,22,100);
    // }

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
                // GameOver();
                yield break;
            }
        }
    } 

    public void CheckScent()
    {
        if(ScentOptionScript.roseSelected)
        {
            prefab = rose;
            gameBgList = new List<Sprite>(roseBgList);
        } else if (ScentOptionScript.vanillaSelected)
        {
            prefab = vanilla;
            gameBgList = new List<Sprite>(vanillaBgList);
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
