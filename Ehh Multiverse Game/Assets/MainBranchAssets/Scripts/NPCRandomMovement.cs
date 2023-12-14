using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float minPauseTime = 1.0f; // Minimum time in seconds to pause between movements
    public float maxPauseTime = 2.0f; // Maximum time in seconds to pause between movements
    public float minTimeToPause = 1.0f;
    public float maxTimeToPause = 3.0f;


    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private float pauseTime;
    private float timeToPause;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = GetRandomDirection().normalized;
        SetRandomPauseTime(); // Set the initial random pause time
        Pause();
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            // If not paused, continue moving
            Vector2 moveVelocity = currentDirection * moveSpeed;
            rb.velocity = moveVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0, 8) * 45.0f;
        return Quaternion.Euler(0, 0, angle) * Vector2.up;
    }

    private void SetRandomPauseTime()
    {
        pauseTime = Random.Range(minPauseTime, maxPauseTime);
        Debug.Log(pauseTime);
    }

    private void SetRandomTimeToNextPause()
    {
        timeToPause = Random.Range(minPauseTime, maxPauseTime);
        Debug.Log(timeToPause);
    }

    private void Pause()
    {
        isPaused = true;
        Debug.Log("Pause");
        if (isPaused)
        {
            // Change direction after the pause ends
            currentDirection = GetRandomDirection().normalized;
            SetRandomPauseTime(); // Set a new random pause time
            SetRandomTimeToNextPause();
            Invoke("TogglePause", pauseTime);

            timeToPause += pauseTime;
            Invoke("Pause", timeToPause);
        }
    }

    private void TogglePause()
    {
        isPaused = false;
    }

    // Handle collisions with any objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect off the collision normal
        currentDirection = Vector2.Reflect(currentDirection, collision.contacts[0].normal);
    }
}