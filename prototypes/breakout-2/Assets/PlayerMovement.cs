
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=5;
    public float maxX = 7.5f;         // Speed of the paddle movement
    public float padding = 0.5f;      // Padding to avoid the paddle being exactly on the edge
    
    private Rigidbody2D rb;           // Reference to the Rigidbody2D component
    private float movementHorizontal; // Horizontal input value


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component on the paddle
    }

    void Update()
    {
        movementHorizontal = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        if((movementHorizontal>0 && transform.position.x<maxX)  || (movementHorizontal<0 && transform.position.x > -maxX))
        {
            transform.position +=Vector3.right*movementHorizontal*speed*Time.deltaTime;
        }
    }
}
