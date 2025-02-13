using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenEndlessMode : MonoBehaviour
{
    // Opens the endless mode of the game where the bricks fall but there is lives
    public void Endless()
    {
        // Load the scene named "Level 2" or whatever your level 2 scene is called
        SceneManager.LoadScene("Endless");
    }
}