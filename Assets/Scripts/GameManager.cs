using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public static GameManager instance;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] TextMeshProUGUI gameoverText;

    private void OnEnable()
    {
        PlayerMovement.PlayerDied += GameOver;
        WaveManager.PlayerWon += GameOver;
    }
    private void OnDisable()
    {
        PlayerMovement.PlayerDied -= GameOver;
        WaveManager.PlayerWon -= GameOver;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else Destroy(gameObject);
    }

    void GameOver(string msg)
    {
        Cursor.lockState = CursorLockMode.None;
        gameoverPanel.SetActive(true);
        gameoverText.text = msg;
        Time.timeScale = 0;
    }
}
