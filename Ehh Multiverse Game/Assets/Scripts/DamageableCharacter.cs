using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public GameObject healthText;
    public GameObject door;

    public float _health = 3f;
    bool isAlive = true;
    bool _targetable = true;
    float invincibleTimeElapsed = 0f;

    public bool disableSimulation = false;
    public bool CanTurnInvincible = false;
    public float invincibilityTime = .25f;
    public bool _invincible = false;

    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get
        {
            return _targetable;
        }
        set
        {
            _targetable = value;

            if (disableSimulation)
            {
                rb.simulated = false;
            }

            physicsCollider.enabled = value;
        }
    }

    public bool Invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;
            if (_invincible)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        animator.SetBool("isAlive", true);
    }

    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Debug.Log("Character hit for " + damage);
            Health -= damage;

            if(CanTurnInvincible)
            {
                Invincible = true;
            }
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Debug.Log("Character hit for " + damage);
            Health -= damage;

            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (CanTurnInvincible)
            {
                Invincible = true;
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }
}
