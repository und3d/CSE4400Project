using UnityEngine;
using System.Collections;


public class AIBehavior : MonoBehaviour
{
    private MovementController movementController;
    private BombController bombController;
    private float decisionCooldown = 2f;
    private float bombPlacementCooldown = 5f;
    private bool isEscaping = false;
    private Vector2 lastBombPosition = Vector2.zero;
    private Vector2 currentDirection = Vector2.zero;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        bombController = GetComponent<BombController>();
    }

    private void Start()
    {
        StartCoroutine(DecisionLoop());
        StartCoroutine(BombPlacementLoop());
    }

    private void OnEnable()
    {
        bombController.OnBombPlaced += HandleBombPlaced;
    }

    private void OnDisable()
    {
        bombController.OnBombPlaced -= HandleBombPlaced;
    }

    private IEnumerator DecisionLoop()
    {
        while (true)
        {
            if (!isEscaping)
            {
                Vector2 newDirection = GetRandomDirection();
                if (CanMoveInDirection(newDirection))
                {
                    movementController.SetDirection(newDirection, ChooseSpriteRenderer(newDirection));
                }
            }
            yield return new WaitForSeconds(decisionCooldown);
        }
    }

    private IEnumerator BombPlacementLoop()
    {
        // Add a random delay before the first bomb placement to stagger the AI's actions
        yield return new WaitForSeconds(Random.Range(0f, bombPlacementCooldown));

        while (true)
        {
            if (!isEscaping && CanPlaceBomb())
            {
                bombController.TryPlaceBomb();
            }

            // Add randomness to each bomb placement cooldown
            float randomCooldown = Random.Range(0.5f * bombPlacementCooldown, 1.5f * bombPlacementCooldown);
            yield return new WaitForSeconds(randomCooldown);
        }
    }


    private void HandleBombPlaced(Vector2 bombPosition)
    {
        lastBombPosition = bombPosition;
        StartCoroutine(EscapeFromBomb(bombPosition));
    }

    private IEnumerator EscapeFromBomb(Vector2 bombPosition)
    {
        isEscaping = true;
        Vector2 escapeDirection = GetEscapeDirection(bombPosition);
        movementController.SetDirection(escapeDirection, ChooseSpriteRenderer(escapeDirection));

        yield return new WaitForSeconds(4f); // Adjust as needed

        isEscaping = false;
    }

    private Vector2 GetEscapeDirection(Vector2 bombPosition)
    {
        Vector2 aiPosition = transform.position;
        Vector2 directionAwayFromBomb = (aiPosition - bombPosition).normalized;

        // Try to move in the opposite direction of the bomb
        if (CanMoveInDirection(directionAwayFromBomb))
        {
            return directionAwayFromBomb;
        }

        // If that's not possible, try any other safe direction
        Vector2[] possibleDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (var direction in possibleDirections)
        {
            if (CanMoveInDirection(direction))
            {
                return direction;
            }
        }

        return Vector2.zero; // No safe direction found (improve this logic based on game design)
    }

    private bool CanMoveInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        return hit.collider == null || (!hit.collider.CompareTag("Wall") && !hit.collider.CompareTag("Bomb")); //these tags don't work properly... :(
    }

    private bool CanPlaceBomb()
    {
        Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (var direction in directions)
        {
            if (CanMoveInDirection(direction))
            {
                return true;
            }
        }
        return false;
    }

    private Vector2 GetRandomDirection()
    {
        if (Random.Range(0, 2) > 0 && currentDirection != Vector2.zero)
        {
            return currentDirection; // 50% chance to continue in the same direction
        }

        int choice = Random.Range(0, 4);
        switch (choice)
        {
            case 0: currentDirection = Vector2.up; break;
            case 1: currentDirection = Vector2.down; break;
            case 2: currentDirection = Vector2.left; break;
            case 3: currentDirection = Vector2.right; break;
        }
        return currentDirection;
    }

    private AnimatedSpriteRenderer ChooseSpriteRenderer(Vector2 direction)
    {
        if (direction == Vector2.up) return movementController.spriteRendererUp;
        if (direction == Vector2.down) return movementController.spriteRendererDown;
        if (direction == Vector2.left) return movementController.spriteRendererLeft;
        if (direction == Vector2.right) return movementController.spriteRendererRight;
        return movementController.spriteRendererDown; // Default
    }
}
