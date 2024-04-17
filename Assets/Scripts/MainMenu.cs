using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton; // Reference to the Play button
    public Button quitButton; // Reference to the Quit button

    void Start()
    {
        // Add listeners for button click events
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame()
    {
        // Load your game scene or perform any play-related actions
        Debug.Log("Play button clicked. Starting the game...");
        SceneManager.LoadScene("GameScene");
        // Example: SceneManager.LoadScene("GameScene");
    }

    void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit button clicked. Quitting the game...");
        Application.Quit();
    }
}
