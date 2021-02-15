using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform _player;

    private SpriteRenderer _thisSprite;
    private SpriteRenderer _playerSprite;

    private Color _color;

    [Header("时间控制参数")] public float activeTime; // 显示时间
    public float activeStart; // 开始显示时间

    [Header("不透明度控制")] private float _alpha;
    public float alphaSet; // 初始值
    public float alphaMultiplier;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _thisSprite = GetComponent<SpriteRenderer>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();

        _alpha = alphaSet;

        _thisSprite.sprite = _playerSprite.sprite;
        transform.position = _player.position;
        transform.localScale = _player.localScale;
        transform.rotation = _player.rotation;

        activeStart = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        _alpha *= alphaMultiplier;
        _color = new Color(1, 1, 1, _alpha);

        _thisSprite.color = _color;

        if (Time.time >= activeStart + activeTime)
        {
            // 返回对象池
            ShadowPool.Instance.ReturnPool(gameObject);
        }
    }
}