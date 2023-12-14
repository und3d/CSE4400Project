using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public float spawnForce;
    public SpriteRenderer spriteRenderer;
    public AudioClip pickupSFX;

    private ItemData itemToGive;

    void Start()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();

        rig.AddForce(Random.insideUnitCircle * spawnForce, ForceMode2D.Impulse);
    }

    public void SetItem (ItemData item)
    {
        itemToGive = item;
        spriteRenderer.sprite = item.Icon;
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemToGive);
            AudioManager.Instance.PlayPlayerSound(pickupSFX);
            Destroy(gameObject);
        }
    }
}
