using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    private float speed = 10;
    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    public void StopMovement()
    {
        isMoving = false; // Stop the movement of the animal/obstacle
    }
}
