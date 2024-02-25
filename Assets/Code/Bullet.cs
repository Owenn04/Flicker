using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 0 || other.gameObject.layer == 3) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Destroy(gameObject, 5.0f);
    }
}
