using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject flashlight;
    public GameObject flashlight_UI;
    private bool flashOn = true;
    public GameObject UVflashlight;
    public GameObject UVflashlight_UI;
    public GameObject bulletPrefab;
    public float force = 20f;
    public int ammoCount = 13;
    public Text ammoText;
    RaycastHit2D lightShine;
    public float flashlightLength = 6; 
    int flashlightMask;
    public GameObject gun;
    public bool graveMoved;
    public GameObject extraGun;

    // Update is called once per frame

    void Start() {
        graveMoved = false;
        extraGun.SetActive(false);
        flashlight.gameObject.SetActive(true);
        UVflashlight.gameObject.SetActive(false);
        flashlight_UI.gameObject.SetActive(true);
        UVflashlight_UI.gameObject.SetActive(false);
        flashlightMask = LayerMask.GetMask("Terrain");
    }

    void Update()
    {           

        lightShine = Physics2D.Raycast(transform.position, transform.right, 6f, flashlightMask);
        if (lightShine.collider != null) {
            flashlightLength = (float)(lightShine.distance + 0.5);
            if (lightShine.collider.gameObject.tag == "Moving Grave" && UVflashlight.gameObject.activeSelf && !graveMoved) {
                StartCoroutine(slowMove(lightShine.collider.gameObject));
                graveMoved = true;
            }
        } else {
            flashlightLength = 6;
        }
        if (flashlight.gameObject.activeSelf == true) {
            flashlight.gameObject.GetComponent<Light2D>().pointLightOuterRadius = flashlightLength;
        } else {
            UVflashlight.gameObject.GetComponent<Light2D>().pointLightOuterRadius = flashlightLength;
        }


        if (Input.GetButtonDown("Fire1")) {   
            if (ammoCount > 0) {
                Shoot();
                gun.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!flashOn) {
                flashlight.gameObject.SetActive(true);
                flashlight_UI.gameObject.SetActive(true);
                UVflashlight.gameObject.SetActive(false);
                UVflashlight_UI.gameObject.SetActive(false);
                flashOn = true;
            } else {
                flashlight.gameObject.SetActive(false);
                flashlight_UI.gameObject.SetActive(false);
                UVflashlight.gameObject.SetActive(true);
                UVflashlight_UI.gameObject.SetActive(true);
                flashOn = false;
            }
        }
        
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.right * force, ForceMode2D.Impulse);
        ammoCount -= 1;
        ammoText.text = ammoCount.ToString();
    }

    public IEnumerator slowMove(GameObject moved) {
        extraGun.SetActive(true);
        for (int i = 0; i < 40; i++) {
            moved.transform.position += new Vector3(0, (float)0.05,0); 
            yield return new WaitForSeconds(0.075f);
        }
    }

}
