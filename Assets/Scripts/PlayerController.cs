using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Collider2D coll;

    public Collider2D disColl;
    // public AudioSource jumpAudio;
    // public AudioSource hurtAudio;
    // public AudioSource cherryAudio;

    public Transform cellingCheck;
    public Transform groundCheck;

    public float speed;
    public float jumpForce;

    public LayerMask ground;

    public int cherry = 0;
    public Text cherryNum;

    private bool isHurt;
    private bool isGround, isJump;
    private bool jumpPressed;


    private int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        cherryNum.text = cherry.ToString();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (!isHurt)
        {
            GroundMovement();
            Jump();
        }

        SwitchAnimatior();
    }


    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        if (horizontalMove != 0) // 更改左右移动时角色朝向
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

        Crouch();
    }

    // 更改动画
    void SwitchAnimatior()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))
        {
            animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        }

        if (isGround)
        {
            animator.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            animator.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }

        if (isHurt) // 受伤
        {
            animator.SetBool("hurting", true);
            animator.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f) // 弹开速度变为0
            {
                animator.SetBool("hurting", false);
                isHurt = false;
            }
        }
        else if (isGround)
        {
            animator.SetBool("falling", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // 碰撞触发器
    {
        // 收集物品
        if (other.CompareTag("Collections"))
        {
            SoundManager.instance.CollectionsAudio();
            other.GetComponent<Animator>().Play("isGot");
        }

        if (other.CompareTag("DeadLine"))
        {
            // GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) // 消灭敌人
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("falling")) // 只有在下落时，才能消灭敌人
            {
                enemy.JumpOn();
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("jumping", true);
            }
            else if (transform.position.x < other.gameObject.transform.position.x) // 受伤
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
            else if (transform.position.x > other.gameObject.transform.position.x) // 受伤
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
        }
    }

    void Crouch() // 下蹲动画
    {
        // 无障碍物的时候可以执行
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                animator.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else
            {
                animator.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }

        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            --jumpCount;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            --jumpCount;
            jumpPressed = false;
        }
    }


    void Restart() // 游戏重开
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherryCount()
    {
        cherry++;
    }
}