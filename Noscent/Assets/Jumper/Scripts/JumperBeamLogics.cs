using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBeamLogics : MonoBehaviour
{
    public JumperLogics Logics;
    public bool powerBeam;
    public bool playerInBeam;

    public SpriteRenderer beam;

    Color32 redBeam = new Color32(255, 66,66,125);
    Color32 blueBeam = new Color32(114,130,243,125);

    private void Start() {
        beam = GetComponent<SpriteRenderer>();

    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     // Debug.Log("Player entered " + this.name);
    //     if(other.gameObject.CompareTag("Player")) playerInBeam = true;
    // }

    // private void OnTriggerExit2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Player")) playerInBeam = false;
    //     // Debug.Log("Player left " + this.name);
    // }

    public void ChangeBeam(bool flag)
    {
        powerBeam = flag;
        // if(powerBeam) beam.color = blueBeam;
        // else beam.color = redBeam;
    }

    private void Update() {
        if(Logics.gamePlaying)
        {
            if(powerBeam && playerInBeam)
            {
                Logics.playerInBeam = true;
                Debug.Log("Point multiplier activated");
            }

            if(Logics.playerInBeam && !powerBeam || !playerInBeam)
            {
                Logics.playerInBeam = false;
                Logics.PlayerLogics.smelling = false;
            }
        }
    }
}
