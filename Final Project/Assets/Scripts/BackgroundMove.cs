using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float speed = 10;
    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    public void StopMovement()
    {
        isMoving = false; // Stop the background movement
    }

    public void StartMovement()
    {
        isMoving = true; // Resume the background movement
    }
}
