using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class light_scripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject extraGun;
    public bool grave1Moved;
    public bool grave2Moved;
    public bool otherMoved;
    int flashlightMask;
    RaycastHit2D lightShine;
    public GameObject UVflashlight;
    void Start() {
        flashlightMask = LayerMask.GetMask("Terrain");
        //Setting up the moving grave
        grave1Moved = false;
        grave2Moved = false;
        extraGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        lightShine = Physics2D.Raycast(transform.position, transform.right, 6f, flashlightMask);
        if (lightShine.collider != null) {
            // If statement moves grave when UV light hits the moving grave
            if (lightShine.collider.gameObject.tag == "Moving Grave 1" && UVflashlight.gameObject.activeSelf && !grave1Moved) {
                StartCoroutine(slowMove(lightShine.collider.gameObject));
                grave1Moved = true;
            }
            if (lightShine.collider.gameObject.tag == "Moving Grave 2" && UVflashlight.gameObject.activeSelf && !grave2Moved) {
                StartCoroutine(slowMove(lightShine.collider.gameObject));
                grave2Moved = true;
            }
        }
    }

    //Script for moving grave
    public IEnumerator slowMove(GameObject moved) {
        extraGun.SetActive(true);
        for (int i = 0; i < 40; i++) {
            moved.transform.position += new Vector3(0, (float)0.05,0); 
            yield return new WaitForSeconds(0.075f);
        }
    }
}
