using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText; // Reference to the Game Over text UI
    public TextMeshProUGUI youWinText; // Reference to the "You Win" text UI
    public int countdownTime = 60;
    private bool isGameOver = false;
    private bool timerRunning = true;

    private PlayerController playerController;
    private SpawnManager spawnManager;
    private BackgroundMove backgroundMove;

    void Start()
    {
        // Initialize score and start the countdown
        UpdateScore(0);
        StartCoroutine(CountdownTimer());

        // Find references to other components
        playerController = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        backgroundMove = FindObjectOfType<BackgroundMove>();

        // Ensure the Game Over and You Win text are hidden at the start
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(false);
        }
    }

    public void AddScore(int value)
    {
        if (isGameOver) return; // Prevent adding score after the game ends

        score += value;
        UpdateScore(score);

        // Check if the player has reached 45 points
        if (score >= 45 && !isGameOver)
        {
            YouWin();
        }
    }

    void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    // Coroutine for the countdown timer
    IEnumerator CountdownTimer()
    {
        int timeLeft = countdownTime;

        while (timeLeft > 0 && timerRunning && !isGameOver)
        {
            timerText.text = "Time Left: " + timeLeft + "s";
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        if (timeLeft <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    public void StopGame()
    {
        timerRunning = false; // Stops the countdown
        StopAllGameplay(); // Call the method to stop gameplay elements
    }

    void GameOver()
    {
        if (isGameOver) return; // Avoid multiple game over conditions

        isGameOver = true;
        timerText.text = "Time's up!";
        Debug.Log("Game Over!");

        // Show the Game Over message
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // Enable the Game Over text
        }

        // Stop all gameplay elements
        StopAllGameplay();
        if (playerController != null)
        {
            playerController.StopPlayerAnimation(); // Ensure you have this method in PlayerController
        }
    }

    void YouWin()
    {
        if (isGameOver) return; // Avoid multiple win conditions

        isGameOver = true;
        timerRunning = false; // Stop the timer when the player wins
        Debug.Log("You Win!");

        // Show the "You Win" message
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(true); // Enable the You Win text
        }

        // Stop all gameplay elements
        StopAllGameplay();
        if (playerController != null)
        {
            playerController.StopPlayerAnimation(); // Ensure you have this method in PlayerController
        }
    }

    void StopAllGameplay()
    {
        // Stop player movement
        if (playerController != null)
        {
            playerController.StopPlayerMovement();
        }

        // Stop object spawning
        if (spawnManager != null)
        {
            spawnManager.StopSpawning();
        }

        // Stop background movement
        if (backgroundMove != null)
        {
            backgroundMove.StopMovement();
        }
    }
}
