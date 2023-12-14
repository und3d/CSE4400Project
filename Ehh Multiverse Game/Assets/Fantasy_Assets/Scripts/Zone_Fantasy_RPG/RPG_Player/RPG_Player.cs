using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RPG_Player : MonoBehaviour
{
    // Editor viewed values
    public float moveSpeed;

    [Header("Components")]
    public Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    public MouseUtilities mouseUtilities;

    // Private value
    private Vector2 moveInput;

    void Update ()
    {
        Vector2 mouseDirection = mouseUtilities.GetMouseDirection(transform.position);

        spriteRenderer.flipX = mouseDirection.x < 0;
    }

    // Updated 
    void FixedUpdate ()
    {
        Vector2 velocity = moveInput * moveSpeed;
        rig.velocity = velocity;
    }

    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

}
