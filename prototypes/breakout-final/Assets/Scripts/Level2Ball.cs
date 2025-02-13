using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Ball : MonoBehaviour
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

    public TrailRenderer trailRenderer;
    [Range(0f, 1f)] public float trailTransparency = 1f; 

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
        SetTrailTransparency(trailTransparency);
    }

    void Update()
    {
        SetTrailTransparency(trailTransparency);

        if (!isBallActive && Input.GetKeyDown(KeyCode.Space))
        {
            DropBall();
        }

        // Debugging: Check ball position
        Debug.Log("Ball Position Y: " + transform.position.y);

        if (isBallActive && transform.position.y < minY)
        {
            Debug.Log("Ball Dropped Below minY");  // DEBUG MESSAGE

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

    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); 
            score += 10; 
            scoreTxt.text = score.ToString("00000"); 
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            ChangeBallDirection(collision); 
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); 
        Debug.Log("Game Over! No lives left.");
    }

    void ResetBall()
    {
        transform.position = Vector3.zero; 
        rb.linearVelocity = Vector2.zero; // ✅ FIXED: Use velocity instead of linearVelocity
        rb.isKinematic = true; 
        isBallActive = false; 
    }

    void DropBall()
    {
        rb.isKinematic = false; 
        isBallActive = true; 
        rb.linearVelocity = Vector2.down * ballBounciness; // ✅ FIXED: Use velocity instead of linearVelocity
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

    void SetTrailTransparency(float transparency)
    {
        if (trailRenderer != null)
        {
            Color currentColor = trailRenderer.startColor;
            trailRenderer.startColor = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);
            trailRenderer.endColor = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);
        }
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
}
