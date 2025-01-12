using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Add this for TextMeshPro

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    [Header("Collection UI")]
    public TextMeshProUGUI itemCountText;
    public TextMeshProUGUI returnInstructionText;

    void Start()
    {
        Cursor.visible = false;
        // Initialize return instruction as hidden
        if (returnInstructionText != null)
            returnInstructionText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                Cursor.visible = false;
            }
        }
    }

    // Add this method to update the collection UI
    public void UpdateCollectionUI(int currentItems, int requiredItems, bool canReturn)
    {
        if (itemCountText != null)
            itemCountText.text = $"Items: {currentItems}/{requiredItems}";

        if (returnInstructionText != null)
        {
            returnInstructionText.gameObject.SetActive(canReturn);
            if (canReturn)
            {
                returnInstructionText.text = "All items collected! Return to start!";
            }
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
}