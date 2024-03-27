using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_scripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject extraGun;
    public bool graveMoved;
    int flashlightMask;
    RaycastHit2D lightShine;
    public GameObject UVflashlight;
    void Start() {
        flashlightMask = LayerMask.GetMask("Terrain");
        //Setting up the moving grave
        graveMoved = false;
        extraGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        lightShine = Physics2D.Raycast(transform.position, transform.right, 6f, flashlightMask);
        if (lightShine.collider != null) {
            // If statement moves grave when UV light hits the grave
            if (lightShine.collider.gameObject.tag == "Moving Grave" && UVflashlight.gameObject.activeSelf && !graveMoved) {
                StartCoroutine(slowMove(lightShine.collider.gameObject));
                graveMoved = true;
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
