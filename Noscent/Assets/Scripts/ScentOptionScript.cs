using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScentOptionScript : MonoBehaviour
{
    public GameObject PlayPanel;

    public Image appleBTN;
    public Image cloveBTN;
    public Image coffeeBTN;
    public Image garlicBTN;
    public Image orangeBTN;
    public Image soapBTN;

    public Image easyBTN;
    public Image mediumBTN;
    public Image hardBTN;

    public bool appleSelected;
    public bool cloveSelected;
    public bool coffeeSelected;
    public bool garlicSelected;
    public bool orangeSelected;
    public bool soapSelected;
    public bool scentSelected;

    public bool easySelected;
    public bool mediuSelected;
    public bool hardSelected;
    public bool difficultySelected;

    string scentText;


    void allScentWhite()
    {
        appleBTN.color = Color.white;
        cloveBTN.color = Color.white;
        coffeeBTN.color = Color.white;
        garlicBTN.color = Color.white;
        orangeBTN.color = Color.white;
        soapBTN.color = Color.white;
        Debug.Log("allScentWhite ran");
    }

    void allDifficultyWhite()
    {
        easyBTN.color = Color.white;
        mediumBTN.color = Color.white;
        hardBTN.color = Color.white;
    }
    
    void allScentFalse()
    {
        appleSelected = false;
        cloveSelected = false;
        coffeeSelected = false;
        garlicSelected = false;
        orangeSelected = false;
        soapSelected = false;
    }
    
    void allDifficultyFalse()
    {
        easySelected = false;
        mediuSelected = false;
        hardSelected = false;
    }

    public void SelectScent(Image caller)
    {
        allScentWhite();
        caller.color = new Color32(37,188,42, 100);
        scentSelected = true;
        if(caller.name == "Apple")
        {
            allScentFalse();
            appleSelected = true;
            scentText = "Apple";
            Debug.Log(appleSelected);
        } else if (caller.name == "Clove")
        {
            allScentFalse();
            cloveSelected = true;
            scentText = "Clove";
        } else if (caller.name == "Coffee")
        {
            allScentFalse();
            coffeeSelected = true;
            scentText = "Coffee";
        } else if (caller.name == "Garlic")
        {
            allScentFalse();
            garlicSelected = true;
            scentText = "Garlic";
        } else if (caller.name == "Orange")
        {
            allScentFalse();
            orangeSelected = true;
            scentText = "Orange";
        } else if (caller.name == "Soap")
        {
            allScentFalse();
            soapSelected = true;
            scentText = "Soap";
        }
    }

    public void DifficultySelection(Image caller)
    {
        allDifficultyWhite();
        difficultySelected = true;
        caller.color = new Color32(37,188,42,100);
        if(caller.name == "Easy")
        {
            allDifficultyFalse();
            easySelected = true;
        } else if (caller.name == "Medium")
        {
            allDifficultyFalse();
            mediuSelected = true;
        } else if (caller.name == "Hard")
        {
            allDifficultyFalse();
            hardSelected = true;
        }
    }
    
    public void ContinueBTN()
    {
        if(scentSelected && difficultySelected)
        {
            PlayPanel.SetActive(true);
            PlayPanel.GetComponent<PlayPanelScript>().UpdateScentText(scentText);
            this.gameObject.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name == "Dodger")
        {
            if(scentSelected)
            {
                PlayPanel.SetActive(true);
                PlayPanel.GetComponent<PlayPanelScript>().UpdateScentText(scentText);
                this.gameObject.SetActive(false);
            }
        }
    }
}
