using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UILogics : MonoBehaviour
{

    // GAME VERSION 1.2
    //Finder edition


    // public GameObject BackBTN;

    // Start is called before the first frame update
    public TextMeshProUGUI anoText;
    public TextMeshProUGUI cdText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI panelTitle;
    public GameObject htpPanel;
    
    

    
    void Start()
    {
        WhiteText();
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

    public void BackBTN()
    {
        SceneManager.LoadScene("StartMenu");
        
    }

    public void AnouncementText(string newText)
    {
        anoText.text = newText;
    }

    void WhiteText()
    {
        if(SceneManager.GetActiveScene().name == "Pong")
        {
            timeText.color = Color.white;
            scoreText.color = Color.white;
            GameObject.Find("TimeRemainingText").GetComponent<TextMeshProUGUI>().color = Color.white;
            GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>().color = Color.white;
            GameObject.Find("BackText").GetComponent<TextMeshProUGUI>().color = Color.white;

        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
