using UnityEngine;

public class BrickBounce : MonoBehaviour
{
    public float bounceForce = 5f; // Adjusts the strength of the brick's movement

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the Brick! Please add one.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // Ensure it's the ball
        {
            if (rb != null)
            {
                // Reflect the brick's movement away from the collision point
                Vector2 reflectDir = Vector2.Reflect(rb.linearVelocity, collision.contacts[0].normal);
                
                // Apply the reflection with a bounce force
                rb.linearVelocity = reflectDir.normalized * bounceForce;
            }
        }
    }
}
