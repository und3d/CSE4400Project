using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Takes and handles input movement for the player character
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 700f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public float moveDrag = 15f;
    public float stopDrag = 25f;
    public GameObject swordHitbox;

    Vector2 moveInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    bool isMoving = false;

    public bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);

            if (isMoving)
            {
                rb.drag = moveDrag;
            }
            else
            {
                rb.drag = stopDrag;
            }
        }
    }

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
        if (canMove == true && moveInput != Vector2.zero)
        {
            // Move animation and add velocity

            // Accelerate the player while run direction is pressed (limited by rigidbody linear drag)

            rb.AddForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

            // Control whether looking left or right
            if(moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                if (swordHitbox != null)
                {
                    gameObject.BroadcastMessage("IsFacingRight", true);
                }
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                if (swordHitbox != null)
                {
                    gameObject.BroadcastMessage("IsFacingRight", false);
                }
            }

            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (swordHitbox != null)
        {
            animator.SetTrigger("swordAttack");
        }
    }

    void OnNextScene()
    {
        int index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(index + 1);
    }

    void LockMovement()
    {
        canMove = false;
    }

    void UnlockMovement()
    {
        canMove = true;
    }

    void AllLivesLost()
    {
        if (PersistentController.Instance.DungeonLives > 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon_0");
            PersistentController.Instance.DungeonLives -= 1;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Zone_0");
            if (PersistentController.Instance.GlobalLives > 1)
            {
                PersistentController.Instance.GetComponent<AudioSource>().Stop();
                PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.Zone0Music;
                PersistentController.Instance.GetComponent<AudioSource>().Play();
                PersistentController.Instance.GlobalLives -= 1;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                PersistentController.Instance.GlobalLives = 3;
                PersistentController.Instance.GetComponent<AudioSource>().Stop();
                PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.MenuMusic;
                PersistentController.Instance.GetComponent<AudioSource>().Play();
            }
        }
    }
}
