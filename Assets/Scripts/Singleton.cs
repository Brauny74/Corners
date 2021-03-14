using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Классы, наследованный от этого класса доступны по атрибуту instance откуда угодно.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    public bool DestroyOnLoad;

    protected static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }
}
