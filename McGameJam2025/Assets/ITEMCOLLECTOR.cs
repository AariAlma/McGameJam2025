using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private HashSet<string> collectedItems = new HashSet<string>();
    public int requiredItems = 5;
    private bool canWin = false;

    // Reference to your pause menu
    public PauseMenu pauseMenuUI;

    // Reference to start point
    public Transform startPoint;

    void Start()
    {
        // Initialize UI at start
        if (pauseMenuUI != null)
        {
            pauseMenuUI.UpdateCollectionUI(0, requiredItems, false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            string itemType = collision.gameObject.name;
            if (!collectedItems.Contains(itemType))
            {
                collectedItems.Add(itemType);
                Debug.Log($"Collected {itemType}! Total: {collectedItems.Count}/{requiredItems}");
                Destroy(collision.gameObject);

                if (collectedItems.Count >= requiredItems)
                {
                    canWin = true;
                }

                // Update UI through pause menu
                if (pauseMenuUI != null)
                {
                    pauseMenuUI.UpdateCollectionUI(collectedItems.Count, requiredItems, canWin);
                }
            }
        }

        if (collision.CompareTag("StartPoint") && canWin)
        {
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("You won!");
        // Add your win condition logic here
    }
}