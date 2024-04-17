using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]private Button homeButton;
    [SerializeField]private Button replayButton;
    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(GoToMainMenu);
        replayButton.onClick.AddListener(ReplayGame);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1;
        // Load the main menu scene when the home button is clicked
        SceneManager.LoadScene("Menu");
    }
    void ReplayGame()
    {
        Time.timeScale = 1;
        Debug.Log("Replay");
        // Reload the current scene to replay the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
