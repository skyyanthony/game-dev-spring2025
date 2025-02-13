using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void BreakoutLevel2()
    {
        // Load the scene named "Level 2" or whatever your level 2 scene is called
        SceneManager.LoadScene("BreakoutLevel2");
    }
}
