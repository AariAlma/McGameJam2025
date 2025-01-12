using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load your game scene - replace "GameScene" with your actual game scene name
        SceneManager.LoadScene("GameCutScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}