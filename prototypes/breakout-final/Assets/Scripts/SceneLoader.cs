using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Ensure we are not reloading the AudioScene
        if (sceneName != "AudioScene")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
