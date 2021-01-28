using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D other) // 碰撞到门
    {
        if (other.CompareTag("Player"))
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 从门那里离开
    {
        if (other.CompareTag("Player"))
        {
            enterDialog.SetActive(false);
        }
    }
}
