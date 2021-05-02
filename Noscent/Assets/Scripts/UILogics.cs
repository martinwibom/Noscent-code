using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class UILogics : MonoBehaviour
{

    // GAME VERSION 1.3
    //Jumper & Finder addition


    // public GameObject BackBTN;

    // Start is called before the first frame update
    public TextMeshProUGUI anoText;
    public TextMeshProUGUI cdText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI panelTitle;
    public GameObject htpPanel;
    public GameObject RedCross;

    public GameObject[] hearts = new GameObject[5];

    public int life;

    

    
    void Start()
    {
        WhiteText();
        NoDifficulty();

    }

    public void htpBTN()
    {
        htpPanel.SetActive(true);
        panelTitle.text = "How to play";
    }

    IEnumerator TwoSeconds()
    {
        cdText.text = "2";

        yield return new WaitForSeconds(1f);

        cdText.text = "1";

        yield return new WaitForSeconds(1f);
    
        cdText.text = "GO!";

        yield return new WaitForSeconds(1f);

        cdText.text = "";

        yield break;

    }


    public void SetLife(int lifeAmount)
    {
        int removeLife = 5 - lifeAmount;
        for (int i = 0; i < removeLife; i++)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        foreach(GameObject heart in hearts.Reverse())
        {
            if(heart.activeInHierarchy == true)
            {
                StartCoroutine("RemoveHeart", heart);
                return;
            }
        }
    }


    IEnumerator RemoveHeart(GameObject heart)
    {
        heart.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        heart.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        heart.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        heart.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        heart.SetActive(false);
    }

    public IEnumerator SelectedScent(string scent)
    {
        anoText.text =  scent + " selected.";

        yield return new WaitForSeconds(2);

        anoText.text = "";
    }

    public void UpdateTime(int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateScoreJumper(int newScore)
    {
        scoreText.text = newScore.ToString() + "/15";
    }

    public void BackBTN()
    {
        SceneManager.LoadScene("StartMenu");
        
    }

    public void UpdateRound(int round)
    {
        timeText.text = round.ToString() + "/5";
    }

    public void AnouncementText(string newText)
    {
        anoText.text = newText;
    }

    void WhiteText()
    {
        if(SceneManager.GetActiveScene().name == "Pong" ||SceneManager.GetActiveScene().name == "Finder" || SceneManager.GetActiveScene().name == "Jumper" )
        {
            timeText.color = Color.white;
            scoreText.color = Color.white;
            GameObject.Find("TimeRemainingText").GetComponent<TextMeshProUGUI>().color = Color.white;
            GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>().color = Color.white;
            GameObject.Find("BackText").GetComponent<TextMeshProUGUI>().color = Color.white;

        }
    }

    void NoDifficulty()
    {
        if(SceneManager.GetActiveScene().name == "Dodger")
        {
            RedCross.SetActive(true);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
