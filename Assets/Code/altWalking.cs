using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles player movement and collision as well as the players stats (hp, ammo count, etc.)
public class altWalking : MonoBehaviour
{

    public Rigidbody2D playerRigidBody;
    private Vector2 moveInput;
    public Camera cam;
    public int playerHealth = 3;
    public bool hasMoonGem = false;
    public bool hasExitGem = false;
    public GameObject HP_3;
    public GameObject HP_2;
    public GameObject HP_1;
    public GameObject HP_0;
    public SpriteRenderer sprite;
    public GameObject gameOverMenu;
    public Shooting shootingScript;

    bool isInvincible = false;
    Vector2 mousePos;
    // Update is called once per frame

    void Start() {
        gameOverMenu.SetActive(false);
        HP_2.gameObject.SetActive(false);
        HP_3.gameObject.SetActive(true);
        HP_1.gameObject.SetActive(false);
        HP_0.gameObject.SetActive(false);
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        moveInput.Normalize();

        playerRigidBody.velocity = moveInput * 6;  

        Vector2 lookDir = mousePos - playerRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRigidBody.rotation = angle;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Extra Gun") {
            shootingScript.ammoCount += 13;
            shootingScript.ammoText.text = shootingScript.ammoCount.ToString();
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "health pickup") {
            playerHealth = 3;
            other.gameObject.SetActive(false);
        } else if (other.gameObject.tag == "moon gem") {
            hasMoonGem = true;
            other.gameObject.SetActive(false);
            Debug.Log("has gem");
        }

        if (other.gameObject.tag == "Enemy" && !isInvincible) {
            playerHealth -= 1;

            StartCoroutine(InvincibilityTimer());
            other.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(FlashRed());

                isInvincible = true;
        } else{
            isInvincible = false;
        }

            if (playerHealth == 3) {
                HP_3.gameObject.SetActive(true);
                HP_2.gameObject.SetActive(false);
                HP_1.gameObject.SetActive(false);
                HP_0.gameObject.SetActive(false);
            } else if (playerHealth == 2) {
                HP_3.gameObject.SetActive(false);
                HP_2.gameObject.SetActive(true);
                HP_1.gameObject.SetActive(false);
                HP_0.gameObject.SetActive(false);
            } else if (playerHealth == 1) {
                HP_3.gameObject.SetActive(false);
                HP_2.gameObject.SetActive(false);
                HP_1.gameObject.SetActive(true);
                HP_0.gameObject.SetActive(false);
            } else if (playerHealth == 0) {
                HP_3.gameObject.SetActive(false);
                HP_2.gameObject.SetActive(false);
                HP_1.gameObject.SetActive(false);
                HP_0.gameObject.SetActive(true);
                gameOver();
            }
        
    }

    IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(1f);
        
    }

    public IEnumerator FlashRed() {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver() {
        gameOverMenu.SetActive(true);
        AudioSource gunSound = gameObject.transform.Find("FirePoint").GetComponent<AudioSource>(); 
        gunSound.enabled = false;
        Invoke("destroyEnemies", 0.5f);

    }

    public void destroyEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
            Destroy(enemy);
    }
}
