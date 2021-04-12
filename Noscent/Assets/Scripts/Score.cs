using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI text;

    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        score++;
        text.text = score.ToString();
    }

    public void RemoveScore()
    {
        score--;
        text.text = score.ToString();
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        text.text = newScore.ToString();
    }

    public void Reset()
    {
        score = 0;
        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
