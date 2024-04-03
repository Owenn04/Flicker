using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 8f;
    public float moveSpeed = 1f;
    private Transform player;
    public int enemyHealth = 3;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;

    public float raycastLength = 2f;
    private LayerMask terrainLayer;

    private float wanderTimer;
    private float wanderInterval = 3f;
    private float wanderRadius = 5f;

    private Vector2 currentWanderTarget;
    private Vector2 wanderOrigin;

    private bool isChasing = false; // Track if the zombie is chasing the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        terrainLayer = LayerMask.GetMask("Terrain");

        wanderOrigin = transform.position;
        ResetWanderTimer();
        SetNewWanderTarget();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            Debug.Log("is chasing false");
            isChasing = false;
            WanderAround();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastLength, terrainLayer);
        Debug.DrawRay(transform.position, direction * raycastLength, Color.red);

        if (hit.collider != null)
        {
            rb.velocity = new Vector2(direction.y, -direction.x) * moveSpeed;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
            FaceDirection(direction);
        }
    }

    void WanderAround()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            SetNewWanderTarget();
            ResetWanderTimer();
        }

        Vector2 direction = (currentWanderTarget - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastLength, terrainLayer);

        if (hit.collider != null)
        {
            rb.velocity = new Vector2(direction.y, -direction.x) * moveSpeed;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
            FaceDirection(direction);
        }
    }

    void FaceDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void SetNewWanderTarget()
    {
        currentWanderTarget = wanderOrigin + Random.insideUnitCircle.normalized * wanderRadius;
    }

    void ResetWanderTimer()
    {
        wanderTimer = wanderInterval;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyHealth -= 1;
            StartCoroutine(FlashRed());
            if (enemyHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}