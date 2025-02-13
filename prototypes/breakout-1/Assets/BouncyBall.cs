using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public float speed = 5f;  // Ball speed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure the ball launches in a random direction
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1).normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball hit: " + collision.gameObject.name); // Debugging collision info

        // Maintain constant speed
        rb.linearVelocity = rb.linearVelocity.normalized * speed;

        // Check if the ball hit a brick
        if (collision.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Brick destroyed: " + collision.gameObject.name); // Log brick destruction
            Destroy(collision.gameObject); // Destroy the brick
        }

        // If the ball hits the paddle, adjust its direction
        if (collision.gameObject.CompareTag("Paddle"))
        {
            AdjustBallDirection(collision.contacts[0].normal);
        }
    }

    // Adjusts the ball's direction based on collision normal
    void AdjustBallDirection(Vector2 collisionNormal)
    {
        Vector2 newDirection = Vector2.Reflect(rb.linearVelocity.normalized, collisionNormal);
        rb.linearVelocity = newDirection * speed; // Maintain speed after bounce
    }
}
