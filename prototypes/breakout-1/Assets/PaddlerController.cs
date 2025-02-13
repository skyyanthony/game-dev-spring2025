using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;  // Speed of the paddle
    public float boundary = 7.5f;  // Screen boundary to prevent moving off-screen

    void Update()
    {
        // Get player input
        float move = Input.GetAxis("Horizontal"); // Left/Right Arrow or A/D keys
        
        // Move the paddle
        transform.position += new Vector3(move * speed * Time.deltaTime, 0, 0);

        // Restrict movement within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -boundary, boundary);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }
}
