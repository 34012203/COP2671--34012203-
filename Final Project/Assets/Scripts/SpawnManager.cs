using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private bool isGameOver = false; 

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void Update()
    {
       
        if (isGameOver)
        {
            CancelInvoke("SpawnObstacle"); 
        }
    }

    void SpawnObstacle()
    {
        if (!isGameOver) 
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject randomObstacle = obstaclePrefabs[randomIndex];
            Instantiate(randomObstacle, spawnPos, randomObstacle.transform.rotation);
        }
    }

    
    public void EndGame(bool playerWon)
    {
        isGameOver = true; 
        Debug.Log("Game Over Called"); 
    }

    
    public void StopSpawning()
    {
        isGameOver = true; 
        CancelInvoke("SpawnObstacle"); 
    }
}
