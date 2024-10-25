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
    public bool hasWon = false; 
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
        else if (collision.gameObject.CompareTag("Obstacle") && !hasWon) 
        {
            Debug.Log("Player hit an obstacle. Game Over!");
            gameOver = true;
            TriggerDeathScene(); 
            EndGame(false);
        }
        else if (collision.gameObject.CompareTag("WinningItem"))
        {
            Debug.Log("Player collected the winning item! You Win!");
            gameOver = true;
            hasWon = true; 
            EndGame(true);
            collision.gameObject.SetActive(false); 
        }
    }

    void EndGame(bool playerWon)
    {
        StopAllAnimalMovement();
        StopBackgroundMovement();
        StopPlayerMovement(); 
        StopPlayerAnimation(); 

        
        FindObjectOfType<ScoreManager>().StopGame();

        FindObjectOfType<GameManager>().EndGame(playerWon);
        FindObjectOfType<SpawnManager>().EndGame(playerWon);
    }

    void StopAllAnimalMovement()
    {
        AnimalMove[] animals = FindObjectsOfType<AnimalMove>(); 
        foreach (AnimalMove animal in animals)
        {
            animal.StopMovement(); 
        }
    }

    void StopBackgroundMovement()
    {
        BackgroundMove background = FindObjectOfType<BackgroundMove>(); 
        if (background != null)
        {
            background.StopMovement();
        }
    }

    public void StopPlayerMovement()
    {
        
        playerRb.velocity = Vector3.zero;          
        playerRb.angularVelocity = Vector3.zero;   
        playerRb.isKinematic = true;                
        playerRb.useGravity = false;                

       
        enabled = false; 
    }

    public void TriggerDeathScene()
    {
        if (!hasWon) 
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 2);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    
    public void StopPlayerAnimation()
    {
        
        playerAnim.SetBool("IsRunning", false); 
        playerAnim.SetBool("IsJumping", false); 
        playerAnim.SetBool("Death_b", false); 
        playerAnim.Play("Idle");
    }
}
