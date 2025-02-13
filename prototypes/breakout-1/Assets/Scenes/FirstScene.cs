using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    // Function to load "Normal1" scene
    public void LoadSceneNormal1()
    {
        SceneManager.LoadScene("Normal1");
    }
}
