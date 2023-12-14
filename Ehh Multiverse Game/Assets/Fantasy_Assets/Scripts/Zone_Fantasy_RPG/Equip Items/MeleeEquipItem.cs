using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEquipItem : EquipItem
{
   public LayerMask hitLayerMask;
   public Animator anim;
   private float lastAttackTime;

   public AudioClip swingSFX;

   public override void OnUse()
   {
    MeleeWeaponItemData i = item as MeleeWeaponItemData;

    if(Time.time - lastAttackTime < i.AttackRate)
        return;

    lastAttackTime = Time.time;
    
    anim.SetTrigger("Attack");

    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, i.Range, hitLayerMask);

    if(hit.collider != null)
    {
        IDamagable damagable = hit.collider.GetComponent<IDamagable>();

        if(damagable != null)
        {
            damagable.TakeDamage(i.Damage);
        }
    }
    
   }
}
