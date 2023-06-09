using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject attackArea;
    
    private IState currentState;
    private Character target;
    public Character Target => target;
    private bool isRight = true;

    
    private void Update()
    {
        if(currentState != null && !IsDead)
        {
            currentState.OnExcute(this);
        }
    }


    // override OnInit
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthbar.gameObject);
        Destroy(gameObject);
    }
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }


    public void ChangeState(IState state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        
        currentState = state;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character target)
    {
        this.target = target;
        if(isTargetInRage())
        {
            ChangeState(new AttackState());
        }
        else if(target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }



    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    } 
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public bool isTargetInRage()
    {
        if(target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }else{
            return false;
        }
        
        
    }

    // onTriggerEnter 2
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
     private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    
    
}
