using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float delay = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;

    private IState currentState;

    private bool isRight = true;

    private Character target;
    public Character Target => target;

    private void Update()
    {
        if(currentState != null)
        {
            currentState.onExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());
        DeactivateAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        StopMoving();
        ChangeState(null);
        base.OnDeath();
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnimation(MyConst.RUN);
        rb.velocity = transform.right * speed;
    }

    public void StopMoving()
    {
        ChangeAnimation(MyConst.IDLE);
        rb.velocity = Vector3.zero;
    }

    public void Attack()
    {
        ChangeAnimation(MyConst.ATTACK);
        ActivateAttack();
        Invoke(nameof(DeactivateAttack), delay);
    }

    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyConst.ENEMY_WALL)
        {
            ChangeDirection(!isRight);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        {
            if (Target != null)
            {
                ChangeState(new PatrolState());
            }
            else
            {
                ChangeState(new IdleState());
            }
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private void ActivateAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactivateAttack()
    {
        attackArea.SetActive(false);
    }
}
