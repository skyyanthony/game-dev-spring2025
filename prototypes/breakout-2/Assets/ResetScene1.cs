using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene1 : MonoBehaviour
{
    // Method to reset the current scene
    public void ResetScene()
    {
        // Get the current active scene's name
        string sceneName = SceneManager.GetActiveScene().name;
        
        // Reload the scene by its name, which will reset everything
        SceneManager.LoadScene(sceneName);
    }

    // You can also use this method to reset on key press or another event
    void Update()
    {
        // For testing: Press 'R' to reset the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }
}
