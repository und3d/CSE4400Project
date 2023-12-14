using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;

    public float damage = 1f;
    public float knockbackForce = 15f;
    public float moveSpeed = 500f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
    }

    private void FixedUpdate()
    {
        if (damageableCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            Vector2 playerDirection = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.AddForce(playerDirection * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            Vector3 parentPosition = transform.position;

            Vector2 direction = (col.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
            }
            else
            {
                knockback = direction * knockbackForce * 50;
                damageableObject.OnHit(damage, knockback);
            }
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDamageable");
        }
    }
}
