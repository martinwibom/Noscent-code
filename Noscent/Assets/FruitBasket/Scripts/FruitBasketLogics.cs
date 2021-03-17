using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitBasketLogics : MonoBehaviour
{

    public GameObject apple;
    public GameObject amount;

    public int score;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Play()
    {
        StartCoroutine("GameSequence");
    }


    IEnumerator GameSequence()
    {
        while (true)
        {
            SpawnObject();

            yield return new WaitForSeconds(1f);
        }
    }

    public void Goal()
    {
        score++;
        amount.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    
    public void SpawnObject()
    {
        Instantiate(apple, new Vector3(Random.Range(-6.35f, 6.35f), 3.20f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
