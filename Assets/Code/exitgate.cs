using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class exitgate : MonoBehaviour
{
    public altWalking walkingScript;
    
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collided");
        if (other.gameObject.tag == "Player" && walkingScript.hasExitGem) {
            Debug.Log("Inside");
            gameObject.SetActive(false);
        }
    }
}
