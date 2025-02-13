using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu1 : MonoBehaviour
{
    // Opens the endless mode of the game where the bricks fall but there is lives
    public void OpenMenu()
    {
        // load the menu
        SceneManager.LoadScene("Menu");
    }
}