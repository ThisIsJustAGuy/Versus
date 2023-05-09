using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 250;
    public float defSpeed = 250;
    public float jumpForce = 8;
    private Rigidbody2D RB;
    private bool doubleJump = false;
    private Animator animator;
    private PlayerCombat pc;
    bool stop = false;
    float horizontalMove;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerCombat>();
    }
    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        if (gameObject.name == "Player1")
        {
            horizontalMove = Input.GetAxisRaw("P1_Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("P2_Horizontal");
        }
        if (horizontalMove != 0 && Time.timeScale != 0 && !pc.isDead)
        {
            stop = true;
            transform.rotation = Quaternion.Euler(0f, (horizontalMove == -1 ? 180f : 0f), 0f);
            RB.velocity = new Vector2(horizontalMove * moveSpeed * Time.deltaTime, RB.velocity.y);
        }
        else if (stop)
        {
            RB.velocity = new Vector2(0f, RB.velocity.y);
            stop = false;
        }
    }
    private void Move()
    {
        if (RB.velocity.y == 0 && Time.timeScale != 0 && !pc.isDead)
        {
            doubleJump = true;
            if (gameObject.name == "Player1")
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsSlamming", false);
            }
        }
        if (gameObject.name == "Player1")
        {
            if (Input.GetButtonDown("P1_Jump") && Mathf.Abs(RB.velocity.y) < 0.01f && Time.timeScale != 0 && !pc.isDead)
            {
                animator.SetBool("IsJumping", true);
                RB.AddForce(new Vector2(0, jumpForce * RB.mass), ForceMode2D.Impulse);
            }
            else if (Input.GetButtonDown("P1_Jump") && doubleJump && Time.timeScale != 0 && !pc.isDead)
            {
                animator.SetBool("IsJumping", true);
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(new Vector2(0, jumpForce * RB.mass), ForceMode2D.Impulse);
                doubleJump = false;
            }

            if (Input.GetButtonDown("P1_Slam") && RB.velocity.y != 0 && Time.timeScale != 0 && !pc.isDead)
            {
                RB.velocity = new Vector2(0, 0);
                RB.AddForce(new Vector2(0, -jumpForce * 1.7f), ForceMode2D.Impulse);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsSlamming", true);
                doubleJump = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("P2_Jump") && Mathf.Abs(RB.velocity.y) < 0.01f && Time.timeScale != 0 && !pc.isDead)
            {
                RB.AddForce(new Vector2(0, jumpForce * RB.mass), ForceMode2D.Impulse);
            }
            else if (Input.GetButtonDown("P2_Jump") && doubleJump && Time.timeScale != 0 && !pc.isDead)
            {
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(new Vector2(0, jumpForce * RB.mass), ForceMode2D.Impulse);
                doubleJump = false;
            }

            if (Input.GetButtonDown("P2_Slam") && RB.velocity.y != 0 && Time.timeScale != 0 && !pc.isDead)
            {
                RB.velocity = new Vector2(0, 0);
                RB.AddForce(new Vector2(0, -jumpForce * 1.7f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }
}
