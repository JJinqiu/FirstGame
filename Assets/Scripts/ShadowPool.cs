using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool Instance;

    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue<GameObject> _availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        FillPool();
    }

    private void FillPool()
    {
        for (var i = 0; i < shadowCount; ++i)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            // 取消启用，返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject _gameObject)
    {
        _gameObject.SetActive(false);

        _availableObjects.Enqueue(_gameObject);
    }

    public GameObject GetFromPool()
    {
        if (_availableObjects.Count == 0)
        {
            FillPool();
        }

        var outShadow = _availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}