using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instatic
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    /// <summary>
    /// 判断当前泛型单例模式是否生成  生成为true 未生成false
    /// </summary>
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    /// <summary>
    /// 当物体被销毁时清空当前静态类的变量
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}