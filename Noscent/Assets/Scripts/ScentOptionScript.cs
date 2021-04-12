using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScentOptionScript : MonoBehaviour
{
    public GameObject PlayPanel;

    public Image appleBTN;
    public Image cloveBTN;
    public Image coffeeBTN;
    public Image garlicBTN;
    public Image orangeBTN;
    public Image soapBTN;

    public bool appleSelected;
    public bool cloveSelected;
    public bool coffeeSelected;
    public bool garlicSelected;
    public bool orangeSelected;
    public bool soapSelected;
    public bool scentSelected;

    string scentText;


    void allWhite()
    {
        appleBTN.color = Color.white;
        cloveBTN.color = Color.white;
        coffeeBTN.color = Color.white;
        garlicBTN.color = Color.white;
        orangeBTN.color = Color.white;
        soapBTN.color = Color.white;
        Debug.Log("AllWhite ran");
    }
    
    void allFalse()
    {
        appleSelected = false;
        cloveSelected = false;
        coffeeSelected = false;
        garlicSelected = false;
        orangeSelected = false;
        soapSelected = false;
    }

    public void SelectScent(Image caller)
    {
        allWhite();
        caller.color = new Color32(37,188,42, 100);
        scentSelected = true;
        if(caller.name == "Apple")
        {
            allFalse();
            appleSelected = true;
            scentText = "Apple";
            Debug.Log(appleSelected);
        } else if (caller.name == "Clove")
        {
            allFalse();
            cloveSelected = true;
            scentText = "Clove";
        } else if (caller.name == "Coffee")
        {
            allFalse();
            coffeeSelected = true;
            scentText = "Coffee";
        } else if (caller.name == "Garlic")
        {
            allFalse();
            garlicSelected = true;
            scentText = "Garlic";
        } else if (caller.name == "Orange")
        {
            allFalse();
            orangeSelected = true;
            scentText = "Orange";
        } else if (caller.name == "Soap")
        {
            allFalse();
            soapSelected = true;
            scentText = "Soap";
        }
    }
    
    public void ContinueBTN()
    {
        if(scentSelected)
        {
            PlayPanel.SetActive(true);
            PlayPanel.GetComponent<PlayPanelScript>().UpdateScentText(scentText);
            this.gameObject.SetActive(false);
        }
    }
}
