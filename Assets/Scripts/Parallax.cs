using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camera2D;
    public float moveRate;
    private float startPointX;
    private float startPointY;
    public bool lockY;

    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector3(startPointX + camera2D.position.x * moveRate, transform.position.y,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(startPointX + camera2D.position.x * moveRate,
                startPointY + camera2D.position.y * moveRate, transform.position.z);
        }
    }
}