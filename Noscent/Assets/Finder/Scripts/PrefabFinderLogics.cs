using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFinderLogics : MonoBehaviour
{
    FinderLogics Logics;

    public bool correctPrefab;
    public bool clickable;
    public bool clicked;

    public SpriteRenderer sr;
    public SpriteRenderer bg;

    public Sprite prefab;
    // public Sprite blank;
    // public Sprite anon;
    public Sprite wrong;

    void Start()
    {
        // sr = this.GetComponent<SpriteRenderer>();
        Logics = GameObject.Find("Logics").GetComponent<FinderLogics>();
    }

    // private void Update() {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if(correctPrefab)
    //             Debug.Log("THIS WORKS");
    //     }
    // }




    void OnMouseDown() 
    {
        if(clickable)
        {
            if(correctPrefab)
            {
                Debug.Log("CORRECT!!");
                PrefabSprite(prefab);
                Logics.UpdateScore(1);
                // Logics.UpdateStreak(true);
                Logics.numberOfCorrect++;
            } else
            {
                WrongSprite();
                // Logics.UpdateStreak(false);
                // Logics.UpdateScore(-1);
                Logics.LoseLife();
            }
        }
    }

    // public void AnonSprite()
    // {
    //     sr.sprite = anon;
    // }
    public void GreenBG()
    {
        bg.color = Color.green;
    }

    public void RedBG()
    {
        bg.color = Color.red;
    }

    public void WhiteBG()
    {
        bg.color = Color.white;
    }

    public void BlankSprite()
    {
        sr.sprite = null;
    }
    public void PrefabSprite(Sprite prefab)
    {
        sr.sprite = prefab;
    }
    public void WrongSprite()
    {
        sr.sprite = wrong;
    }
}
