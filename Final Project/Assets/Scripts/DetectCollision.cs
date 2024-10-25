using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public PlayerController playerController;
    public int scoreValue = 5;

    private void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle. Game Over!");
            playerController.gameOver = true;
            playerController.TriggerDeathScene();
            FindObjectOfType<GameManager>().EndGame(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            ScoreManager scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
            scoreManager.AddScore(scoreValue);

            Debug.Log("Projectile hit the obstacle. Destroying obstacle and projectile.");
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
