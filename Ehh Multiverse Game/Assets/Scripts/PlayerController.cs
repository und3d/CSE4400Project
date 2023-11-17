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
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool leftWasLast = false;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            //if movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)    //If a collision is detected and movement input is still detected, allows movement along collision
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("IsMoving", success);
            }
            else
            {
                spriteRenderer.flipX = false;
                animator.SetBool("IsMoving", false);
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", false);
            }

            #region Animation Directions
            //Set direction of sprite to movement direction
            /*
            if (movementInput.x > 0 && movementInput.y > 0)     //Up Right
            {
                spriteRenderer.flipX = false;
                leftWasLast = false;
                animator.SetBool("movingRight", true);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", false);
            }
            else if (movementInput.x < 0 && movementInput.y > 0)     //Up Left
            {
                spriteRenderer.flipX = false;
                leftWasLast = true;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", true);
                animator.SetBool("movingDown", false);
            }
            else if (movementInput.x > 0 && movementInput.y < 0)     //Down Right
            {
                spriteRenderer.flipX = false;
                leftWasLast = false;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", true);
            }
            else if (movementInput.x < 0 && movementInput.y < 0)     //Down Left
            {
                spriteRenderer.flipX = false;
                leftWasLast = true;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", true);
            }
            else if (movementInput.y > 0)                   //Up
            {
                spriteRenderer.flipX = false;
                leftWasLast = false;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", true);
                animator.SetBool("movingDown", false);
            }
            else if (movementInput.y < 0)                   //Down
            {
                spriteRenderer.flipX = false;
                leftWasLast = false;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", true);
            }
            */
            if (movementInput.x < 0)                    //Left
            {
                spriteRenderer.flipX = true;
                leftWasLast = true;
                animator.SetBool("movingRight", false);
                animator.SetBool("movingLeft", true);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", false);
            }
            else if (movementInput.x > 0)                   //Right
            {
                spriteRenderer.flipX = false;
                leftWasLast = false;
                animator.SetBool("movingRight", true);
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingUp", false);
                animator.SetBool("movingDown", false);
            }

            #endregion

            if (leftWasLast)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
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
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        canMove = true;
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
        canMove = false;
    }

    public void SwordAttack()
    {
        if (spriteRenderer.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    void UnlockMovement()
    {
        canMove = true;
    }
}
