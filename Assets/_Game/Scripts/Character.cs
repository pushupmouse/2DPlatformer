using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private CombatText combatTextPrefab;

    private float hp;
    private string currentAnimationName;

    private bool isDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {

    }

    public virtual void OnDeath()
    {
        ChangeAnimation(MyConst.DIE);

        Invoke(nameof(OnDespawn), 1f);
    }

    protected void ChangeAnimation(string animationName)
    {
        if (currentAnimationName != animationName)
        {
            animator.ResetTrigger(animationName);

            currentAnimationName = animationName;

            animator.SetTrigger(currentAnimationName);
        }
    }

    public void OnHit(float damage)
    {
        if(!isDead)
        {
            hp -= damage;
            
            if(isDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.SetNewHealth(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }
}
