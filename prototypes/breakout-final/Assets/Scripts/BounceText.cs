using UnityEngine;
using TMPro;

public class BounceText : MonoBehaviour
{
    public float bounceSpeed = 2.0f;  // Speed of the bounce
    public float bounceHeight = 20.0f; // How high it moves up and down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        // Calculate vertical movement using sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
