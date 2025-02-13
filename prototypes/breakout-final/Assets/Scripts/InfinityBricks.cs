using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class InfinityBricks : MonoBehaviour
{
    public GameObject brickPrefab;
    public Gradient gradient;
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2.0f;
    public float fallSpeed = 2.0f;
    public float spawnRangeX = 8.0f;

    private List<GameObject> brickPool = new List<GameObject>();

    private void Start()
    {
        Time.timeScale = 1; // Ensure the game never pauses
        StartCoroutine(SpawnBricks());
    }

    IEnumerator SpawnBricks()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnBrick();
        }
    }

    void SpawnBrick()
    {
        GameObject newBrick = GetPooledBrick();
        if (newBrick == null)
        {
            newBrick = Instantiate(brickPrefab);
            brickPool.Add(newBrick);
        }

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnHeight = Camera.main.orthographicSize + 1;
        newBrick.transform.position = new Vector3(randomX, spawnHeight, 0);
        newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate(Random.value);
        newBrick.tag = "Brick";
        newBrick.SetActive(true);

        Rigidbody2D rb = newBrick.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = newBrick.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(0, -fallSpeed);
    }

    GameObject GetPooledBrick()
    {
        foreach (GameObject brick in brickPool)
        {
            if (!brick.activeInHierarchy)
            {
                return brick;
            }
        }
        return null;
    }

    public void DestroyBrick(GameObject brick)
    {
        if (brick != null)
        {
            brick.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
