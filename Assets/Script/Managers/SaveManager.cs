using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="data">需要保存的数据</param>
    /// <param name="key">键值  保存文件名</param>
    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data,true);
        PlayerPrefs.SetString(key,jsonData);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="data">需要读取的数据</param>
    /// <param name="key">键值  读取文件名</param>
    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key),data);
        }
    }
}
