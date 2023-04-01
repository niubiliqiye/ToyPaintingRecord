using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class TimedShutdown : MonoBehaviour
{
    private TimerSystem _timer;
    
    [Header("需要计时的长度")] [Tooltip("时间单位为秒  1分钟请输入60秒   1小时请输入3600秒")]
    public float lengthOfTime;
    
    [Header("是否倒计时")] [Tooltip("是否倒计时   true：是  false：否")]
    public bool iscCountDown;
    
    void Start()
    {

    }

    private void OnEnable()
    {
        //创建计时器
        _timer = TimerSystem.CreateTimer();
        _timer.StartTiming(lengthOfTime, iscCountDown, OnComplete, null, true, false, true);
    }

    /// <summary>
    /// 计时完成后回调函数
    /// </summary>
    void OnComplete()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(_timer);
    }
}
