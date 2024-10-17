using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasWon = false; // Flag to track if the player has won
    public GameObject projectilePrefab;
    public float projectileOffset = 1.5f;
    public float heightOffset = 1.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Prevent movement and input after game ends (either win or lose)
        if (!gameOver && !hasWon)
        {
            if (Input.GetKeyDown(KeyCode.J) && isOnGround)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }
        }
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    void ShootProjectile()
    {
        Vector3 projectileSpawnPosition = transform.position + transform.forward * projectileOffset + Vector3.up * heightOffset;
        Instantiate(projectilePrefab, projectileSpawnPosition, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !hasWon) // Only trigger death if player hasn't won
        {
            Debug.Log("Player hit an obstacle. Game Over!");
            gameOver = true;
            TriggerDeathScene(); // Trigger death only when hitting an obstacle
            EndGame(false);
        }
        else if (collision.gameObject.CompareTag("WinningItem"))
        {
            Debug.Log("Player collected the winning item! You Win!");
            gameOver = true;
            hasWon = true; // Set hasWon to true
            EndGame(true);
            collision.gameObject.SetActive(false); // Make the winning item disappear
        }
    }

    void EndGame(bool playerWon)
    {
        StopAllAnimalMovement();
        StopBackgroundMovement();
        StopPlayerMovement(); // Stop player movement when game is over
        StopPlayerAnimation(); // Stop player animation

        // Stop the timer in ScoreManager
        FindObjectOfType<ScoreManager>().StopGame();

        FindObjectOfType<GameManager>().EndGame(playerWon);
        FindObjectOfType<SpawnManager>().EndGame(playerWon);
    }

    void StopAllAnimalMovement()
    {
        AnimalMove[] animals = FindObjectsOfType<AnimalMove>(); // Find all active animals
        foreach (AnimalMove animal in animals)
        {
            animal.StopMovement(); // Stop each animal
        }
    }

    void StopBackgroundMovement()
    {
        BackgroundMove background = FindObjectOfType<BackgroundMove>(); // Find the background movement script
        if (background != null)
        {
            background.StopMovement(); // Stop the background
        }
    }

    public void StopPlayerMovement()
    {
        // Disable the Rigidbody's physics interactions and movement
        playerRb.velocity = Vector3.zero;          // Stop any current movement
        playerRb.angularVelocity = Vector3.zero;   // Stop any current rotation
        playerRb.isKinematic = true;                // Make the Rigidbody kinematic (stops all physics)
        playerRb.useGravity = false;                // Disable gravity

        // Disable player's ability to jump and shoot
        enabled = false; // Disable the entire PlayerController script
    }

    public void TriggerDeathScene()
    {
        if (!hasWon) // Only trigger death if the player hasn't won
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 2);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    // New method to stop player's animation
    public void StopPlayerAnimation()
    {
        // Assuming "IsRunning" and "IsJumping" are the parameters controlling the run and jump animations
        playerAnim.SetBool("IsRunning", false); // Stop running animation
        playerAnim.SetBool("IsJumping", false); // Stop jumping animation
        playerAnim.SetBool("Death_b", false); // Reset death animation if needed
        playerAnim.Play("Idle");
    }
}
