using UnityEngine;
using TMPro;

public class InfinityBall : MonoBehaviour
{
    public float minY = -5.5f; // Minimum Y position before resetting the ball
    public float brickDestroyDelay = 3f; // Delay before bricks below minY are destroyed

    // Ball movement properties
    public float ballBounciness = 20f;
    public float ballMaxSpeed = 15f;
    private float bounceMultiplier = 1.1f;

    private Rigidbody2D rb;
    private bool isBallActive = false;

    // Score UI elements
    public int score = 0;
    public TextMeshProUGUI scoreTxt;

    private Vector3 startPosition;

    // Audio
    private AudioSource audioSource;
    public AudioClip brickHitSound; // Assign in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        startPosition = transform.position;

        ResetBall();
        UpdateScoreText();
    }

    void Update()
    {
        if (!isBallActive && Input.GetKeyDown(KeyCode.Space))
        {
            DropBall();
        }

        if (isBallActive && transform.position.y < minY)
        {
            ResetBall();
        }

        DestroyBricksBelowScreen();
    }

    void ResetBall()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        isBallActive = false;
    }

    void DropBall()
    {
        rb.isKinematic = false;
        isBallActive = true;
        rb.linearVelocity = Vector2.down * ballBounciness;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            rb.linearVelocity *= bounceMultiplier;
            score += 10;
            UpdateScoreText();

            // Play the brick hit sound
            if (audioSource != null && brickHitSound != null)
            {
                audioSource.PlayOneShot(brickHitSound);
            }
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            ChangeBallDirection(collision);
        }
    }

    public void ChangeBallDirection(Collision2D collision)
    {
        Vector2 paddleCenter = collision.collider.bounds.center;
        Vector2 hitPoint = collision.contacts[0].point;

        float hitFactor = (hitPoint.x - paddleCenter.x) / collision.collider.bounds.extents.x;

        float minYVelocity = 3f;
        Vector2 newDirection = new Vector2(hitFactor, 1f).normalized;

        rb.linearVelocity = newDirection * Mathf.Clamp(rb.linearVelocity.magnitude * bounceMultiplier, minYVelocity, ballMaxSpeed);
    }

    void UpdateScoreText()
    {
        if (scoreTxt != null)
        {
            scoreTxt.text = score.ToString("00000");
        }
    }

    void DestroyBricksBelowScreen()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        foreach (GameObject brick in bricks)
        {
            if (brick.transform.position.y < minY)
            {
                Destroy(brick, brickDestroyDelay);
            }
        }
    }
}
