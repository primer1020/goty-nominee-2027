using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 currentPosition;
    private Vector3 velocity;
    private bool wasOnGround;
    private bool onGround;

    void Start()
    {
        wasOnGround = true;
        onGround = true;
        velocity = new Vector3(0, 0, 0);
        startingPosition = gameObject.transform.position;
        currentPosition = startingPosition;
    }

    void ProcessJumping()
    {
        bool tryingToJump = Input.GetKeyDown(KeyCode.Space);
        if (onGround && tryingToJump)
        {
            Debug.Log("Jumping!");
            velocity.y += 2.0f;
            onGround = false;
        }
    }

    void ProcessGravity()
    {
        velocity.y -= Time.deltaTime * 5;
        if (currentPosition.y < startingPosition.y)
        {
            onGround = true;
            velocity.y = 0;
            currentPosition.y = startingPosition.y;
        }
    }

    void ProcessVelocity()
    {
        currentPosition += velocity * Time.deltaTime;
    }

    void Update()
    {
        wasOnGround = onGround;
        ProcessGravity();
        ProcessJumping();
        ProcessVelocity();
        if (wasOnGround != onGround)
        {
            Debug.Log(onGround);
        }
        gameObject.transform.position = currentPosition;
    }
}
