using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs to spawn
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private bool isGameOver = false; // Track if the game is over

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void Update()
    {
        // If the game is over, stop the spawning
        if (isGameOver)
        {
            CancelInvoke("SpawnObstacle"); // Ensure no further calls are made
        }
    }

    void SpawnObstacle()
    {
        if (!isGameOver) // Only spawn if the game isn't over
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject randomObstacle = obstaclePrefabs[randomIndex];
            Instantiate(randomObstacle, spawnPos, randomObstacle.transform.rotation);
        }
    }

    // Call this method to end the spawning
    public void EndGame(bool playerWon)
    {
        isGameOver = true; // Set game over to true
        Debug.Log("Game Over Called"); // Debug log to ensure this is being called
    }

    // Public method to stop spawning
    public void StopSpawning()
    {
        isGameOver = true; // Ensure spawning is halted
        CancelInvoke("SpawnObstacle"); // Stop any further calls to SpawnObstacle
    }
}
