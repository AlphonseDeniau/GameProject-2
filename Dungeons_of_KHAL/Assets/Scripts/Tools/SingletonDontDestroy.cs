using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonDontDestroy<T> : MonoBehaviour where T : Component
{

    private static T _instance = null;
    public static T Instance => _instance;

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(_instance);
        }
        else
            Destroy(gameObject);
    }
}
