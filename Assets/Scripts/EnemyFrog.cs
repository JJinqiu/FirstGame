using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    // private Animator animator;
    private Collider2D coll;

    public LayerMask ground;
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed;
    public float jumpForce;
    private bool faceLeft = true;
    private float leftX;
    private float rightX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement();
        SwitchAnimation();
    }

    void Movement() // 青蛙移动
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(ground)) // 判断是不是在地上
            {
                animator.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }

            if (transform.position.x < leftX)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground)) // 判断是不是在地上
            {
                animator.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }

            if (transform.position.x > rightX)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnimation() // 切换动画
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }

        if (coll.IsTouchingLayers(ground) && animator.GetBool("falling"))
        {
            animator.SetBool("falling", false);
        }
    }
}