using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
    public float knockbackForce = 500f;
    public Vector3 faceRight = new Vector3(0.400000006f, -0.217999995f, 0);
    public Vector3 faceLeft = new Vector3(-0.400000006f, -0.217999995f, 0);

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider is not set");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageableObject = (IDamageable) other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(swordDamage, knockback);
        }
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
