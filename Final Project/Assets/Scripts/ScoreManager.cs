using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWinText;
    public Button restartButton; 
    public int countdownTime = 60;

    private bool isGameOver = false;
    private bool timerRunning = true;

    private PlayerController playerController;
    private SpawnManager spawnManager;
    private BackgroundMove backgroundMove;

    void Start()
    {
        
        UpdateScore(0);
        StartCoroutine(CountdownTimer());

        
        playerController = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        backgroundMove = FindObjectOfType<BackgroundMove>();

        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(false);
        }

       
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }
    }

    public void AddScore(int value)
    {
        if (isGameOver) return; 

        score += value;
        UpdateScore(score);

        
        if (score >= 45 && !isGameOver)
        {
            YouWin();
        }
    }

    void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    
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
        timerRunning = false; 
        StopAllGameplay(); 
    }

    void GameOver()
    {
        if (isGameOver) return; 

        isGameOver = true;
        timerText.text = "Time's up!";
        Debug.Log("Game Over!");

        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); 
        }

        
        StopAllGameplay();
        if (playerController != null)
        {
            playerController.StopPlayerAnimation(); 
        }

        
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true); 
        }
    }

    void YouWin()
    {
        if (isGameOver) return; 

        isGameOver = true;
        timerRunning = false; 
        Debug.Log("You Win!");

        
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(true); 
        }

        
        StopAllGameplay();
        if (playerController != null)
        {
            playerController.StopPlayerAnimation(); 
        }

        
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true); 
        }
    }

    void StopAllGameplay()
    {
       
        if (playerController != null)
        {
            playerController.StopPlayerMovement();
        }

        
        if (spawnManager != null)
        {
            spawnManager.StopSpawning();
        }

        
        if (backgroundMove != null)
        {
            backgroundMove.StopMovement();
        }
    }

    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
