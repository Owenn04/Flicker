using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveactivator : MonoBehaviour
{

    GameObject postgrave;
    public GameObject gravecanvas;
    public GameObject gravecanvaspost;
    public altWalking walkingScript;
    void Start() {
        postgrave = GameObject.FindWithTag("post grave");
        postgrave.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {  
            walkingScript.hasExitGem = true;
            Debug.Log("registered player collision");
            showPre();
            Invoke("hidePre", 2);
            Invoke("hidePost", 4);
            postgrave.gameObject.SetActive(true);
            Debug.Log("Deactivated");
            gameObject.SetActive(false);
        }
    }

    
    public void showPre() {
        gravecanvas.SetActive(true);
        Debug.Log("turned on pre");
    }
    public void hidePre() {
        gravecanvas.SetActive(false);
        Debug.Log("turned off pre");
        gravecanvaspost.SetActive(true);
        Debug.Log("turned on post");
    }
    public void hidePost() {
        gravecanvaspost.SetActive(false);
        Debug.Log("turned off post");
    }
}
