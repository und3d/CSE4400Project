using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RPG_Enemy : Character
{
    public enum State
    {
        Idle,
        Chase,
        Attack
    }

    protected State curState;

    public float moveSpeed;
    public float chaseDistance;

    public ItemData[] dropItems;
    public GameObject dropItemPrefab;
    
    protected GameObject target;

    protected float lastAttackTime;
    protected float targetDistance;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        target = FindObjectOfType<RPG_Player>().gameObject;
    }

    public virtual void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);

        spriteRenderer.flipX = GetTargetDirection().x < 0;

        switch(curState)
        {
            case State.Idle: IdleUpdate(); break;
            case State.Chase: ChaseUpdate(); break;
            case State.Attack: AttackUpdate(); break;
        }
    }

    void ChangeState (State newState)
    {
        curState = newState;
    }

    void IdleUpdate ()
    {
        if(targetDistance <= chaseDistance)
            ChangeState(State.Chase);
    }

    void ChaseUpdate()
    {
        if(InAttackRange())
            ChangeState(State.Attack);
        else if(targetDistance > chaseDistance)
            ChangeState(State.Idle);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    void AttackUpdate()
    {
        if(targetDistance > chaseDistance)
            ChangeState(State.Idle);
        else if(!InAttackRange())
            ChangeState(State.Chase);

        if(CanAttack())
        {
            lastAttackTime = Time.time;
            AttackTarget();
        }
    }

    public virtual void AttackTarget()
    {

    }

    public virtual bool CanAttack()
    {
        return false;
    }

    public virtual bool InAttackRange()
    {
        return false;
    }

    public Vector2 GetTargetDirection ()
    {
        return (target.transform.position - transform.position).normalized;
    }

    public override void Die ()
    {
        DropItems();
        Destroy(gameObject);
    }

    public virtual void DropItems()
    {
        for (int  i = 0; i < dropItems.Length; i++)
        {
            GameObject obj = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<WorldItem>().SetItem(dropItems[i]);
        }
    }
}
