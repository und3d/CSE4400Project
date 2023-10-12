using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Takes and handles input movement for the player character
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if movement input is not 0, try to move
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if(!success)    //If a collision is detected and movement input is still detected, allows movement along collision
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        //Check for potential collisions
        int count = rb.Cast(
            direction,      // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter,     // The settings that determine where a collision can occur on such as layeers to collide with
            castCollisions,     // List of collisions to store the found collisions into after the cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset);     //The amount to cast equal to the movement plus an offset

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
