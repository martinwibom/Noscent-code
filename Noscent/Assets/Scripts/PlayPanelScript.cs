using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayPanelScript : MonoBehaviour
{
    public GameObject ScentOptionPanel;
    public TextMeshProUGUI text;

    public void UpdateScentText(string scent)
    {
        text.text = "Current scent: " + scent;
    }

    public void SelectScent()
    {
        ScentOptionPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }
}
