using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;  // Number of bricks in X and Y directions
    public Vector2 offset;   // Spacing between bricks
    public GameObject brickPrefab;  // Brick prefab to instantiate
    public Gradient gradient;  // Color gradient for bricks

    private GameObject[] bricks;  // Array to track all the brick objects

    private void Awake()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        bricks = new GameObject[size.x * size.y];  // Initialize array to store bricks
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                // Instantiate a new brick at the correct position
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3(i * offset.x, j * offset.y, 0);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));

                // Ensure the brick has the correct tag
                newBrick.tag = "Brick";

                // Store the brick in the array
                bricks[i + j * size.x] = newBrick;
            }
        }
    }

    void Update()
    {
        // Check if all bricks are destroyed
        if (AreAllBricksDestroyed())
        {
            Restart();
        }
    }

    // Check if all bricks are destroyed
    bool AreAllBricksDestroyed()
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

    public void Restart()
    {
        // Restart the level by reloading the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;  // Resume time after reloading
    }
}
