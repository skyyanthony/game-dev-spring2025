using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioLoader : MonoBehaviour
{
    void Start()
    {
        if (FindObjectOfType<PersistentAudio>() == null) // Ensure it's not loaded twice
        {
            SceneManager.LoadScene("AudioScene", LoadSceneMode.Additive);
        }
    }
}
