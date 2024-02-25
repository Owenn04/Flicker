using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 30f; // Range within which the enemy detects the player
    public float moveSpeed = 5f; // Speed at which the enemy moves

    private Transform player; 
    public int enemyHealth = 3;
    private Rigidbody2D rb; 
    public SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // Check if the player is within the detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Calculate the direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;

            // Rotate the enemy to face the player
            transform.right = direction;
        }
        else
        {
            // If the player is not within range, stop moving
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            enemyHealth -= 1;
            StartCoroutine(FlashRed());
            if (enemyHealth == 0) {
                Destroy(gameObject);
            }
        } else {
        // If the enemy collides with something, try to go around it by turning
        transform.Rotate(Vector3.forward * Random.Range(-90f, 90f)); 
        }
    }

    public IEnumerator FlashRed() {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
