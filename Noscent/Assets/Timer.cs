using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60;

    public bool counting;

    private void Start() {
        counting = false;
    }

    // IEnumerator StartCountdown()
    // {
    //     while (true)
    //     {
    //         timeRemaining -= Time.deltaTime;
    //         int time = (int) timeRemaining;
    //         this.GetComponent<TextMeshProUGUI>().text = time.ToString();
    //         // yield return null;
    //         if(timeRemaining <= 0)
    //     {
    //         yield break;
    //     }
    //     }
    //     // yield break;
    // }

    public void ToggleTimer()
    {
        if(!counting)
        {
            counting = true;
        } else if (counting)
        {
            counting = false;
        }
    }

    void Update()
    {
        if(counting)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int time = (int) timeRemaining;
                this.GetComponent<TextMeshProUGUI>().text = time.ToString();
            }
        }
    }
}