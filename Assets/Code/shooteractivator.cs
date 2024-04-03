using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class shooteractivator : MonoBehaviour
{
    public altWalking walkingScript;
    
    Light2D[] shinedLights;
    GameObject vanishingDoor;

    void Start() {
        GameObject receptacle = GameObject.FindWithTag("Receptacle");
        vanishingDoor = GameObject.FindWithTag("Disappearing");
        shinedLights = receptacle.GetComponentsInChildren<Light2D>();
        for (int i = 0; i < shinedLights.Length; i++){
                    shinedLights[i].enabled = false;
                    Debug.Log("added light");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Checking");
        if (other.gameObject.tag == "Player") {   
            Debug.Log("Collided");
            if (walkingScript.hasMoonGem) {
                for (int i = 0; i < shinedLights.Length; i++){
                    shinedLights[i].enabled = true;
                    Debug.Log("enabled");
                }
                vanishingDoor.gameObject.SetActive(false);
            }
            walkingScript.hasMoonGem = false;
        }
    }
}
