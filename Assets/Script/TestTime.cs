using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestTime : MonoBehaviour
{
    //计时器使用模板

    [Header("需要计时的长度")] [Tooltip("时间单位为秒  1分钟请输入60秒   1小时请输入3600秒")]
    public float lengthOfTime;

    [Header("倒计时")] [Tooltip("是否倒计时   true：是  false：否")]
    public bool iscCountDown;

    [Header("时间显示框")] public TextMeshProUGUI timerBox;

    //[Header("开始闪烁")] public bool twinkle;

    private TimerSystem _timer;

    private Color tempColor;

    private UI_Animation uiAnimation;

    private ShuKaKu shuKaKu;

    private void Awake()
    {
        uiAnimation = FindObjectOfType<UI_Animation>();
        shuKaKu = FindObjectOfType<ShuKaKu>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //创建计时器
        _timer = TimerSystem.CreateTimer("testTimer");
        _timer.StartTiming(lengthOfTime, iscCountDown, OnComplete, OnProcess);
    }

    // Update is called once per frame
    void Update()
    {
        if (uiAnimation.isOpen)
        {
            if (_timer._isTimer)
            {
                _timer.PauseTimer();
            }
        }
        else
        {
            if (!_timer._isTimer)
            {
                _timer.ConnitueTimer();
            }
        }
    }

    // 计时结束的回调
    void OnComplete()
    {
        shuKaKu.countDownFinish = true;
        gameObject.SetActive(false);
    }

    // 计时器的进程
    void OnProcess(float p)
    {
        //显示剩余时间
        //Debug.Log(TimerSystem.FormatTime01(_timer.GetTimeNow()));
        
        //显示时间
        //Debug.Log(TimerSystem.FormatTime01(p));
        timerBox.text = TimerSystem.FormatTime01(p);
        ColorUtility.TryParseHtmlString("#CD3D3D", out tempColor);
        if (p < 10f)
        {
            if (timerBox.color != tempColor)
            {
                timerBox.color=tempColor;
            }
        }
    }
}