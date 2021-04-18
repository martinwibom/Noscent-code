using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HowToPlayScript : MonoBehaviour
{
    public UILogics UI;

    public TextMeshProUGUI igoText;
    public TextMeshProUGUI controlText;
    public TextMeshProUGUI stText;


    // Start is called before the first frame update
    void Start()
    {
        LoadText();
        this.gameObject.SetActive(false);
    }
    
    public void LoadText()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "FruitBasket")
        {
            CatcherText();
        } else if (sceneName == "Jumper")
        {
            JumperText();
        } else if (sceneName == "Pong")
        {
            PongerText();
        } else if (sceneName == "Dodger")
        {
            DodgerText();
        } else if(sceneName == "Finder")
        {
            FinderText();
        }
    }

    public void ChangeTitle()
    {
        UI.panelTitle.text = "Select scent";
    }

    public void BackBTN()
    {
        ChangeTitle();
        this.gameObject.SetActive(false);
    }

    void CatcherText()
    {
        igoText.text = "Catch as many falling objects as possible during one minute.";
        controlText.text = "Use right and left arrow keys to control your player.";
        stText.text = "Select the scent in-game that you will be smelling during the game. \nHold the scent material under your nose and smell it throughout the gameplay.";
    }

    void JumperText()
    {
        igoText.text = "Jumper over as many objects as possible during one minute.";
        controlText.text = "Use space bar to jump with your player.";
        stText.text = "Select the scent in-game that you will be smelling during the game. \nHold the scent material under your nose and smell it throughout the gameplay.";
    }

    void PongerText()
    {
        igoText.text = "Bounce the ball into the green goal on the opposite side of the player.";
        controlText.text = "Use up and down arrow keys to control your player.";
        stText.text = "Select the scent in-game that you will be smelling during the game. \nHold the scent material under your nose and smell it throughout the gameplay.";
    }

    void DodgerText()
    {
        igoText.text = "Dodge the objects coming at you and reach the goal line.";
        controlText.text = "Use right and left arrow keys to control your player.";
        stText.text =  "Select the scent in-game that you will be smelling during the game. \nHold the scent material under your nose and move it with the player. \nIf the player is in the... \nLeft lane = Hold the scent material under your left nostril. \nRight lane = Hold the scent material under your right nostril. \nMiddle lane = Hold the scent material under both nostrils.";
    }

    void FinderText()
    {
        igoText.text ="Remember the position of the scent material and click it once the squares go blank. Everytime you get all guesses right, you’ll have an additional one to remember next turn. Try and get as high score as possible.";
        controlText.text = "Use your mouse to click the white squares.";
        stText.text = "Select the scent in-game that you will be smelling during the game. \nWhen you are in the “remembering-phase” of the game, smell your scent material. When you are going to click the empty white squares, stop smelling. \nResume smelling when you are in the “remembering-phase”.";
    }

 
}
