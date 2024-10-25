using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winText; 
    public TextMeshProUGUI loseText; 
    private bool gameHasEnded = false;
    public Button restartButton;

    void Start()
    {
        
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartGame); // Assign the restart functionality to the button
        }
    }

    public void EndGame(bool playerWon)
    {
        if (!gameHasEnded) 
        {
            gameHasEnded = true;

            if (playerWon)
            {
                Debug.Log("You Win!");
                winText.gameObject.SetActive(true); 
                winText.text = "You Win!"; 
            }
            else
            {
                Debug.Log("You Lose!");
                loseText.gameObject.SetActive(true); 
                loseText.text = "You Lose!"; 
            }
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(true);
            }
        }
    }
    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
