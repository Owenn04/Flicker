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

    public PauseMenu pauseMenu;

    // Update is called once per frame

    void Start() {
        // Sets up UI and active flashlight
        flashlight.gameObject.SetActive(true);
        UVflashlight.gameObject.SetActive(false);
        flashlight_UI.gameObject.SetActive(true);
        UVflashlight_UI.gameObject.SetActive(false);
        flashlightMask = LayerMask.GetMask("Terrain");
    }

    void Update()
    {           
        // Detects if light is hitting an object and shortens ray accordingly 
        lightShine = Physics2D.Raycast(transform.position, transform.right, 6f, flashlightMask);
        if (lightShine.collider != null) {
            flashlightLength = (float)(lightShine.distance + 0.5);
        } else {
            flashlightLength = 6;
        }
        if (flashlight.gameObject.activeSelf == true) {
            flashlight.gameObject.GetComponent<Light2D>().pointLightOuterRadius = flashlightLength;
        } else {
            UVflashlight.gameObject.GetComponent<Light2D>().pointLightOuterRadius = flashlightLength;
        }

        // Check if the game is paused before allowing shooting
        if (!pauseMenu.isPaused){
            if (Input.GetButtonDown("Fire1")) {   
                if (ammoCount > 0) {
                    Shoot();
                    gun.gameObject.GetComponent<AudioSource>().Play();
                }
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

    // Script for player shooting
    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.right * force, ForceMode2D.Impulse);
        ammoCount -= 1;
        ammoText.text = ammoCount.ToString();
    }

}
