using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public AudioSource introAudio;  // Drag your AudioSource here

    void Start()
    {
        // Optional: Automatically play the audio when scene starts
        if (introAudio != null)
            introAudio.Play();
    }

    void Update()
    {
        // Check for Enter key press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Load your game scene
            SceneManager.LoadScene("GameScene");
        }
    }
}