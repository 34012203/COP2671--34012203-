using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winText; // Reference to the UI Text component for displaying win message
    public TextMeshProUGUI loseText; // Reference to the UI Text component for displaying lose message
    private bool gameHasEnded = false; // Flag to track if the game has ended

    void Start()
    {
        // Initially hide the win and lose messages
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
    }

    public void EndGame(bool playerWon)
    {
        if (!gameHasEnded) // Prevent multiple calls
        {
            gameHasEnded = true;

            if (playerWon)
            {
                Debug.Log("You Win!");
                winText.gameObject.SetActive(true); // Show win message
                winText.text = "You Win!"; // Display win message
            }
            else
            {
                Debug.Log("You Lose!");
                loseText.gameObject.SetActive(true); // Show lose message
                loseText.text = "You Lose!"; // Display lose message
            }

            // Additional logic for ending the game can be placed here, such as restarting the game.
        }
    }
}
