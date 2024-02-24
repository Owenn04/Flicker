using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject flashlight;
    private bool flashOn = false;
    public GameObject bulletPrefab;

    public float force = 20f;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!flashOn) {
                flashlight.gameObject.SetActive(true);
                flashOn = true;
            } else {
                flashlight.gameObject.SetActive(false);
                flashOn = false;
            }
        }
        
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.right * force, ForceMode2D.Impulse);
    }
}
