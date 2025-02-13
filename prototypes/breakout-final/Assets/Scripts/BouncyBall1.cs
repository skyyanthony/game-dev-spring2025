using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BouncyBall1 : MonoBehaviour
{
    public float minY = -5.5f; 
    public float maxVelocity = 15f;
    private Rigidbody2D rb;

    public int score = 0;
    public int lives = 5;
    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage; 
    public GameObject gameOverPanel;
    public GameObject[] bricks;
    public Button levelTwoButton;

    public float ballBounciness = 20f; 
    public float ballMaxSpeed = 15f; 
    private float bounceMultiplier = 3.1f; 

    private bool isBallActive = false; 

    // ** Audio for Brick Hit **
    public AudioSource audioSource;
    public AudioClip brickHitSound;

    private float minHorizontalVelocity = 2f; // Minimum horizontal speed to avoid straight up/down movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bricks = GameObject.FindGameObjectsWithTag("Brick");

        if (levelTwoButton != null)
        {
            levelTwoButton.gameObject.SetActive(false);
        }

        gameOverPanel.SetActive(false); 
        ResetBall();
        UpdateLivesUI();

        // ** Initialize Audio Source **
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!isBallActive && Input.GetKeyDown(KeyCode.Space))
        {
            DropBall();
        }

        if (isBallActive && transform.position.y < minY)
        {
            if (lives > 0)
            {
                lives--;  
                UpdateLivesUI();
                CheckGameOver();

                if (lives > 0) 
                {
                    ResetBall();
                }
            }
        }

        if (CheckAllBricksDestroyed() && levelTwoButton != null)
        {
            levelTwoButton.gameObject.SetActive(true);
        }

        FixBallGettingStuck(); // Prevents vertical movement locking
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); 
            score += 10; 
            scoreTxt.text = score.ToString("00000"); 

            // ** Play Brick Hit Sound **
            if (brickHitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(brickHitSound);
            }
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            ChangeBallDirection(collision); 
        }
    }

    private bool CheckAllBricksDestroyed()
    {
        foreach (GameObject brick in bricks)
        {
            if (brick != null) return false;
        }
        return true;
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); 
    }

    void ResetBall()
    {
        transform.position = Vector3.zero; 
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

    public void ChangeBallDirection(Collision2D collision)
    {
        Vector2 paddleCenter = collision.collider.bounds.center;
        Vector2 hitPoint = collision.contacts[0].point;

        float hitFactor = (hitPoint.x - paddleCenter.x) / (collision.collider.bounds.extents.x);

        float minYVelocity = 3f;  
        Vector2 newDirection = new Vector2(hitFactor, 1f).normalized;

        rb.linearVelocity = newDirection * Mathf.Clamp(rb.linearVelocity.magnitude * bounceMultiplier, minYVelocity, ballMaxSpeed);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("BreakoutLevel2");
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < livesImage.Length; i++)
        {
            livesImage[i].SetActive(i < lives); 
        }
    }

    void CheckGameOver()
    {
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // **Fixes Ball Getting Stuck in Vertical Motion**
    void FixBallGettingStuck()
    {
        if (isBallActive)
        {
            Vector2 velocity = rb.linearVelocity;

            // If the ball is mostly moving up/down (very low horizontal speed)
            if (Mathf.Abs(velocity.x) < minHorizontalVelocity)
            {
                float newX = (velocity.x >= 0) ? minHorizontalVelocity : -minHorizontalVelocity; // Ensure it moves sideways
                rb.linearVelocity = new Vector2(newX, velocity.y).normalized * Mathf.Clamp(rb.linearVelocity.magnitude, 5f, ballMaxSpeed);
            }
        }
    }
}
