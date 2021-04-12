using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    TextMeshProUGUI cdtext;    

    void Start()
    {
        cdtext = this.GetComponent<TextMeshProUGUI>();
    }

    IEnumerator TwoSeconds()
    {
        cdtext.text = "2";

        yield return new WaitForSeconds(1f);

        cdtext.text = "1";

        yield return new WaitForSeconds(1f);
    
        cdtext.text = "GO!";

        yield return new WaitForSeconds(1f);

        cdtext.text = "";

        yield break;

    }

    public void ResetText()
    {
        cdtext.text = "";
    }

}
