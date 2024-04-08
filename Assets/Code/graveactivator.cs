using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; 

public class graveactivator : MonoBehaviour
{

    GameObject postgrave;
    public GameObject gravecanvas;
    public GameObject gravecanvaspost;
    public altWalking walkingScript;
    public TextMeshProUGUI gemText1;

    void Start() {
        postgrave = GameObject.FindWithTag("post grave");
        postgrave.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {  
            walkingScript.hasExitGem = true;
            Debug.Log("registered player collision");
            showPre();
            Invoke("hidePre", 1);
            Invoke("hidePost", 2);
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
        UpdateUI();
        Invoke("ClearGemText", 2f);
    }
    private void UpdateUI(){
        if(gemText1 != null){
            gemText1.text = "Gems Collected: " + 2 + "/" + 2;
        }
    }
    private void ClearGemText(){
        if (gemText1 != null)
        {
            gemText1.text = "";
        }
    }
}
