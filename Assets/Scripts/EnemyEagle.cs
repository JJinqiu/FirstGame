using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    public Transform topPoint;
    public Transform bottomPoint;
    public float speed;
    private float TopY;
    private float BottomY;

    private bool isUp;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TopY = topPoint.position.y;
        BottomY = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }
}