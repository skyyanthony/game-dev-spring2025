using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStartButton : MonoBehaviour
{
    // Function to load Level 1 when button is clicked
    public void Menu()
    {
        SceneManager.LoadScene("Menu1");
    }
}
