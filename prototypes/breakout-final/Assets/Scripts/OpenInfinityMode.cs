using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInfinityMode: MonoBehaviour
{
    // Opens the endless mode of the game where the bricks fall but there is lives
    public void Infinity()
    {
        // Load the scene named "Level 1" or whatever your level 2 scene is called
        SceneManager.LoadScene("Infinity");
    }
}