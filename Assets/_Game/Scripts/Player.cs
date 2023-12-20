using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distanceDown = 1.1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowRatio = 1.2f;
    [SerializeField] private float jumpForce = 350f;
    [SerializeField] private float delay = 0.5f;

    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private float horizontal;
    private int coinsCollected = 0;
    private int highscore = 0;
    private Vector3 savePoint;

    private void Awake()
    {
        highscore = PlayerPrefs.GetInt(MyConst.HIGHSCORE, 0);
    }

    private void Update()
    {
        if(isDead) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckGrounded();

        

        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Throw();
            }
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            Fall();
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            Run();
        }
        else
        {
            Idle();
        }
    }


    public override void OnInit()
    {
        base.OnInit();
        
        isDead = false;
        isAttacking = false;
        isJumping = false;

        transform.position = savePoint;

        ChangeAnimation(MyConst.IDLE);
        DeactivateAttack();

        Savepoint();
        UIManager.instance.SetCoin(coinsCollected);
        UIManager.instance.SetHighscore(highscore);
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceDown, groundLayer);
        
        return hit.collider != null;
    }

    private void Run()
    {
        if (isGrounded)
        {
            ChangeAnimation(MyConst.RUN);
        }
        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * (isGrounded? speed : speed/slowRatio), rb.velocity.y);

        transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
    }

    private void Idle()
    {
        if (isGrounded)
        {
            ChangeAnimation(MyConst.IDLE);
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        ChangeAnimation(MyConst.ATTACK);
        Invoke(nameof(ResetAnimation), delay);

        ActivateAttack();
        Invoke(nameof(DeactivateAttack), delay);
    }

    public void Throw()
    {
        isAttacking = true;
        ChangeAnimation(MyConst.THROW);
        Invoke(nameof(ResetAnimation), delay);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void Jump()
    {
        isJumping = true;
        ChangeAnimation(MyConst.JUMP);
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void Fall()
    {
        isJumping = false;
        ChangeAnimation(MyConst.FALL);
    }

    private void ResetAnimation()
    {
        isAttacking = false;
        ChangeAnimation(MyConst.IDLE);
    }

    internal void Savepoint()
    {
        savePoint = transform.position;
    }

    private void ActivateAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactivateAttack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyConst.COIN)
        {
            coinsCollected++;
            UIManager.instance.SetCoin(coinsCollected);
            if(coinsCollected > highscore)
            {
                highscore = coinsCollected;
                PlayerPrefs.SetInt(MyConst.HIGHSCORE, highscore);
                UIManager.instance.SetHighscore(highscore);
            }
            Destroy(collision.gameObject);
        }

        if (collision.tag == MyConst.DEATHZONE)
        {
            ChangeAnimation(MyConst.DIE);
            isDead = true;

            Invoke(nameof(OnInit),1f);
        }
    }
}
