using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float brickPassThreshold = 3; // Number of bricks passing the bottom to lose a life
    public float maxVelocity = 15f;
    private Rigidbody2D rb;

    public int score = 0;
    public int lives = 5;
    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject[] bricks;
    public Button levelTwoButton;

    // Public variables for ball's bounciness and speed
    public float ballBounciness = 20f; // Speed at which the ball will launch/drop
    public float ballMaxSpeed = 15f; // Maximum speed the ball can have (adjusted)
    private float bounceMultiplier = 3.1f; // Slightly increase speed after hitting the paddle

    private bool isBallActive = false; // Ball will start inactive

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bricks = GameObject.FindGameObjectsWithTag("Brick");

        if (levelTwoButton != null)
        {
            levelTwoButton.gameObject.SetActive(false);
        }

        ResetBall(); // Start with the ball paused
    }

    void Update()
    {
        if (!isBallActive && Input.GetKeyDown(KeyCode.Space))
        {
            DropBall();
        }

        if (isBallActive && transform.position.y < minY)
        {
            // Reset the ball if it falls outside the screen (below the minY threshold)
            ResetBall();
        }

        if (CheckAllBricksDestroyed() && levelTwoButton != null)
        {
            levelTwoButton.gameObject.SetActive(true);
        }

        // Check for bricks passing below the screen
        HandleBricksPassingBottom();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the ball hits a brick
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreTxt.text = score.ToString("00000");
        }
    }

    private bool CheckAllBricksDestroyed()
    {
        foreach (GameObject brick in bricks)
        {
            if (brick != null)
            {
                return false;
            }
        }
        return true;
    }

    void GameOver() 
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true; // Stop physics simulation
        isBallActive = false;

        // Only deactivate life image if there are lives remaining
        if (lives > 0)
        {
            livesImage[lives - 1].SetActive(false); // Hide the last life image
        }
    }

    void DropBall()
    {
        rb.isKinematic = false; // Re-enable physics
        isBallActive = true;
        rb.linearVelocity = Vector2.down * ballBounciness; // Drop straight down with specified speed
    }

  

    // Function to check if bricks pass below the bottom of the screen
    void HandleBricksPassingBottom()
    {
        int bricksPassedBottom = 0;

        foreach (GameObject brick in bricks)
        {
            if (brick != null && brick.transform.position.y < minY)
            {
                bricksPassedBottom++;
            }
        }

        if (bricksPassedBottom >= brickPassThreshold)
        {
            // Decrease lives if too many bricks pass the bottom
            if (lives > 0)
            {
                lives--;
                livesImage[lives].SetActive(false);
                ResetBall();
            }
            else
            {
                GameOver();
            }
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("BreakoutLevel2");
    }
}
