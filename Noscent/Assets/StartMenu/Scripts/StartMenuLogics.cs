using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playFruitBasket()
    {
        SceneManager.LoadScene("FruitBasket");
    }

    public void playPong()
    {
        SceneManager.LoadScene("Pong");
    }

    public void playJumper()
    {
        SceneManager.LoadScene("Jumper");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void playDodger()
    {
        SceneManager.LoadScene("Dodger");
    }

    public void playFinder()
    {
        SceneManager.LoadScene("Finder");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            QuitApplication();
        }
    }
}
