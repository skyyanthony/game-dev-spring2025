using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    // Opens the endless mode of the game where the bricks fall but there is lives
    public void Menu()
    {
        // load the menu
        SceneManager.LoadScene("Menu");
    }
}