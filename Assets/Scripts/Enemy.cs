using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected AudioSource deathAudio;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death() // 在动画结束后触发
    {
        GetComponent<Collider2D>().enabled = false; // 避免怪物死亡造成二次碰撞
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        deathAudio.Play();
        animator.SetTrigger("death");
    }
}