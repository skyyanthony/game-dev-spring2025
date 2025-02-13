using System.Linq;  
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BrickSpawnerLevel2 : MonoBehaviour
{
    public GameObject brickPrefab;  
    public Gradient gradient;  
    public float minSpawnInterval = 0.5f; 
    public float maxSpawnInterval = 2.0f; 
    public float baseFallSpeed = 2.0f; // Base speed of bricks
    private float fallSpeed; // Updated dynamically
    public float spawnRangeX = 8.0f; 

    public EdgeCollider2D edgeCollider; 
    private int lastScoreMilestone = 0;

    private void Start()
    {
        Time.timeScale = 1;
        fallSpeed = baseFallSpeed;
        StartCoroutine(SpawnBricks());
    }

    IEnumerator SpawnBricks()
    {
        while (true)
        {
            UpdateFallSpeed();

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float spawnHeight = Camera.main.orthographicSize + 1; // Ensure spawn is above screen
            Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

            // Instantiate the brick
            GameObject newBrick = Instantiate(brickPrefab, spawnPosition, Quaternion.identity);
            newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate(Random.value);
            newBrick.tag = "Brick";

            // Ensure the brick has a Rigidbody2D
            Rigidbody2D rb = newBrick.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = newBrick.AddComponent<Rigidbody2D>();
            }

            rb.gravityScale = 0; // Disable gravity
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Apply updated fall speed

            Debug.Log($"Spawned Brick at {spawnPosition}, Falling Speed: {fallSpeed}"); // DEBUG LOG

            if (edgeCollider != null)
            {
                edgeCollider.enabled = false;
                yield return new WaitForSeconds(0.1f);
                edgeCollider.enabled = true;
            }

            Collider2D[] wallColliders = GameObject.FindGameObjectsWithTag("Walls")
                .Select(go => go.GetComponent<Collider2D>())
                .ToArray();

            foreach (Collider2D wallCollider in wallColliders)
            {
                Physics2D.IgnoreCollision(newBrick.GetComponent<Collider2D>(), wallCollider);
            }

            Collider2D paddleCollider = GameObject.FindGameObjectWithTag("Paddle")?.GetComponent<Collider2D>();
            if (paddleCollider != null)
            {
                Physics2D.IgnoreCollision(newBrick.GetComponent<Collider2D>(), paddleCollider);
            }
        }
    }

    void UpdateFallSpeed()
    {
        int currentScore = GameObject.FindGameObjectWithTag("Ball").GetComponent<Level2Ball>().score;

        if (currentScore >= 40 && lastScoreMilestone < 40)
        {
            fallSpeed = baseFallSpeed * 2.0f; 
            lastScoreMilestone = 40;
        }
        else if (currentScore >= 30 && lastScoreMilestone < 30)
        {
            fallSpeed = baseFallSpeed * 1.8f; 
            lastScoreMilestone = 30;
        }
        else if (currentScore >= 20 && lastScoreMilestone < 20)
        {
            fallSpeed = baseFallSpeed * 1.5f; 
            lastScoreMilestone = 20;
        }
        else if (currentScore >= 10 && lastScoreMilestone < 10)
        {
            fallSpeed = baseFallSpeed * 1.2f; 
            lastScoreMilestone = 10;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; 
    }
}
