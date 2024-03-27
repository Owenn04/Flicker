using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class light_scripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject extraGun;
    public bool graveMoved;
    public bool otherMoved;
    int flashlightMask;
    RaycastHit2D lightShine;
    public GameObject UVflashlight;
    public GameObject receptacleLight;
    void Start() {
        flashlightMask = LayerMask.GetMask("Terrain");
        //Setting up the moving grave
        graveMoved = false;
        extraGun.SetActive(false);
        //Setting up light refracting graves
        receptacleLight.GetComponent<Light2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lightShine = Physics2D.Raycast(transform.position, transform.right, 6f, flashlightMask);
        if (lightShine.collider != null) {
            // If statement moves grave when UV light hits the moving grave
            if (lightShine.collider.gameObject.tag == "Moving Grave" && UVflashlight.gameObject.activeSelf && !graveMoved) {
                StartCoroutine(slowMove(lightShine.collider.gameObject));
                graveMoved = true;
            }
            // If statement moves grave when UV light hits the refracting grave
            if (lightShine.collider.gameObject.tag == "Receptacle" && UVflashlight.gameObject.activeSelf && !otherMoved) {
                receptacleLight.GetComponent<Light2D>().enabled = true;
            }
        } else {
            receptacleLight.GetComponent<Light2D>().enabled = false;
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
