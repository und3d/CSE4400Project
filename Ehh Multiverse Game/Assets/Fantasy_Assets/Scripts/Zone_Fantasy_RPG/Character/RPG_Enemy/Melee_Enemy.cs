using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy : RPG_Enemy
{
    public int damage;
    public float attackRange;
    public float attackRate;

    public override void AttackTarget()
    {
        IDamagable damagable = target.GetComponent<IDamagable>();

        if(damagable != null)
            damagable.TakeDamage(damage);
    }

    public override bool CanAttack()
    {
        return (Time.time - lastAttackTime) > attackRate;
    }

    public override bool InAttackRange()
    {
        return targetDistance <= attackRange;
    }
}
