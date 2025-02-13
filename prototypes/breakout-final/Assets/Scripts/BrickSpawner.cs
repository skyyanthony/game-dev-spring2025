using System.Linq;  // Add this at the top of your file

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;  // Brick prefab to instantiate
    public Gradient gradient;  // Color gradient for bricks
    public float minSpawnInterval = 0.5f; // Minimum time between spawns
    public float maxSpawnInterval = 2.0f; // Maximum time between spawns
    public float fallSpeed = 2.0f; // Speed at which bricks fall
    public float spawnRangeX = 8.0f; // Range for random spawn positions across the top of the screen

    public EdgeCollider2D edgeCollider; // Reference to the EdgeCollider2D to disable temporarily

    private void Start()
    {
        Time.timeScale = 1; // Ensure the game is running
        StartCoroutine(SpawnBricks());
    }

    IEnumerator SpawnBricks()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Adjust spawn position to ensure it's above the screen bounds
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float spawnHeight = Camera.main.orthographicSize + 1; // Just above the camera's view height
            Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

            // Instantiate the brick and set its color
            GameObject newBrick = Instantiate(brickPrefab, spawnPosition, Quaternion.identity);
            newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate(Random.value);
            newBrick.tag = "Brick";

            // Add Rigidbody2D and manually control the fall speed
            Rigidbody2D rb = newBrick.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Disable gravity to manually control the fall
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Make the brick fall downwards at the specified speed

            // Temporarily disable the edge collider to avoid immediate collision
            if (edgeCollider != null)
            {
                edgeCollider.enabled = false; // Disable the collider
                yield return new WaitForSeconds(0.1f); // Brief pause to allow brick to fall
                edgeCollider.enabled = true; // Re-enable the collider
            }

            // Ignore collisions with "Walls" tagged objects
            Collider2D[] wallColliders = GameObject.FindGameObjectsWithTag("Walls")
                .Select(go => go.GetComponent<Collider2D>())
                .ToArray();

            foreach (Collider2D wallCollider in wallColliders)
            {
                Physics2D.IgnoreCollision(newBrick.GetComponent<Collider2D>(), wallCollider);
            }

            // Ignore collision with the paddle
            Collider2D paddleCollider = GameObject.FindGameObjectWithTag("Paddle")?.GetComponent<Collider2D>();
            if (paddleCollider != null)
            {
                Physics2D.IgnoreCollision(newBrick.GetComponent<Collider2D>(), paddleCollider);
            }
        }
    }

    void Update()
    {
        // You can check conditions or handle other gameplay logic here.
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; 
    }
}
