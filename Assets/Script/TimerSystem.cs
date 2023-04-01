using System;
using UnityEngine;

namespace Script
{
    public delegate void CompleteEvent();
    public delegate void UpdateEvent(float t);
    public class TimerSystem : MonoBehaviour
    {
        UpdateEvent _updateEvent;
        CompleteEvent _onCompleted;
        
        /// <summary>
        /// 是否打印消息
        /// </summary>
        bool _isLog = true;
        
        /// <summary>
        /// 计时时间
        /// </summary>
        float _timeTarget;
        
        /// <summary>
        /// 开始计时的时间
        /// </summary>
        float _timeStart;
        
        /// <summary>
        /// 计时偏差
        /// </summary>
        float _offsetTime;
        
        /// <summary>
        /// 是否开始计时
        /// </summary>
        public bool _isTimer;
        
        /// <summary>
        /// 计时结束后是否销毁
        /// </summary>
        bool _isDestory = true;
        
        /// <summary>
        /// 计时是否结束
        /// </summary>
        bool _isEnd;
        
        /// <summary>
        /// 是否忽略时间速率
        /// </summary>
        bool _isIgnoreTimeScale = true;
        
        /// <summary>
        /// 是否重复
        /// </summary>
        bool _isRepeate;
        
        /// <summary>
        /// 当前时间 计时中
        /// </summary>
        float _now;
        
        /// <summary>
        /// 倒计时
        /// </summary>
        float _downNow;
        
        /// <summary>
        /// 是否是倒计时
        /// </summary>
        bool _isDownNow = false;
        
        /// <summary>
        /// 暂停的时间
        /// </summary>
        float _pauseTime;

        /// <summary>
        /// 是否使用游戏的真实时间 不依赖游戏的时间速度
        /// Time.realtimeSinceStartup      从游戏开始后所运行的真实时间，不会受时间缩放比例的影响。
        /// Time.time   从游戏开始后所运行的时间，会受时间缩放比例的影响。
        /// </summary>
        float TimeNow
        {
            get { return _isIgnoreTimeScale ? Time.realtimeSinceStartup : Time.time; }
        }

        /// <summary>
        /// 创建计时器
        /// 根据名字可以创建多个计时器对象
        /// <param name="gobjName">计时器名字    默认值为"TimerSystem"</param>
        /// </summary>
        public static TimerSystem CreateTimer(string gobjName = "TimerSystem")
        {
            GameObject g = new GameObject(gobjName);
            TimerSystem timer = g.AddComponent<TimerSystem>();
            return timer;
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="timeTarget">目标时间</param>
        /// <param name="isDownNow">是否是倒计时  默认值:false</param>
        /// <param name="onCompleted">计时完成回调函数    默认值:null</param>
        /// <param name="update">计时器进程回调函数  默认值:null</param>
        /// <param name="isIgnoreTimeScale">是否忽略时间倍数    默认值:true</param>
        /// <param name="isRepeate">是否重复    默认值:false</param>
        /// <param name="isDestory">完成后是否销毁     默认值:false</param>
        /// <param name="offsetTime">计时偏差   默认值:0</param>
        /// <param name="isEnd">计时是否结束  默认值:false</param>
        /// <param name="isTimer">是否开始计时    默认值:true</param>
        public void StartTiming(float timeTarget, bool isDownNow = false, CompleteEvent onCompleted = null, UpdateEvent update = null, bool isIgnoreTimeScale = true, bool isRepeate = false, bool isDestory = false, float offsetTime = 0, bool isEnd = false, bool isTimer = true)
        {
            this._timeTarget = timeTarget;
            this._isIgnoreTimeScale = isIgnoreTimeScale;
            this._isRepeate = isRepeate;
            this._isDestory = isDestory;
            this._offsetTime = offsetTime;
            this._isEnd = isEnd;
            this._isTimer = isTimer;
            this._isDownNow = isDownNow;
            _timeStart = TimeNow;

            if (onCompleted != null)
                _onCompleted = onCompleted;
            if (update != null)
                _updateEvent = update;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (_isTimer)
            {
                _now = TimeNow - _offsetTime - _timeStart;
                _downNow = _timeTarget - _now;
                if (_updateEvent != null)
                {
                    if (_isDownNow)
                    {
                        _updateEvent(_downNow);
                    }
                    else
                    {
                        _updateEvent(_now);
                    }
                }
                if (_now > _timeTarget)
                {
                    if (_onCompleted != null)
                        _onCompleted();
                    if (!_isRepeate)
                        Destory();
                    else
                        ReStartTimer();
                }
            }
        }
        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <returns></returns>
        public float GetTimeNow()
        {
            return Mathf.Clamp(_timeTarget - _now, 0, _timeTarget);
        }

        /// <summary>
        /// 计时结束
        /// </summary>
        public void Destory()
        {
            _isTimer = false;
            _isEnd = true;
            if (_isDestory)
                Destroy(gameObject);
        }

        
        /// <summary>
        /// 暂停计时
        /// </summary>
        public void PauseTimer()
        {
            if (_isEnd)
            {
                if (_isLog) Debug.LogWarning("计时已经结束！");
            }
            else
            {
                if (_isTimer)
                {
                    _isTimer = false;
                    _pauseTime = TimeNow;
                }
            }
        }

        /// <summary>
        /// 继续计时
        /// </summary>
        public void ConnitueTimer()
        {
            if (_isEnd)
            {
                if (_isLog) Debug.LogWarning("计时已经结束！请从新计时！");
            }
            else
            {
                if (!_isTimer)
                {
                    _offsetTime += (TimeNow - _pauseTime);
                    _isTimer = true;
                }
            }
        }

        /// <summary>
        /// 重新计时
        /// </summary>
        public void ReStartTimer()
        {
            _timeStart = TimeNow;
            _offsetTime = 0;
        }

        /// <summary>
        /// 更改目标时间
        /// </summary>
        /// <param name="time"></param>
        public void ChangeTargetTime(float time)
        {
            _timeTarget = time;
            _timeStart = TimeNow;
        }


        /// <summary>
        /// 游戏暂停调用
        /// </summary>
        /// <param name="isPause">是否暂停</param>
        public void OnApplicationPause(bool isPause)
        {
            if (isPause)
            {
                PauseTimer();
            }
            else
            {
                ConnitueTimer();
            }
        }
        
        /// <summary>
        /// 格式化时间
        /// 格式：10:00
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string FormatTime01(float seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(seconds));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = "00:" + ts.Seconds.ToString("00");
            }
            return str;
        }
        
        /// <summary>
        /// 格式化时间
        /// 格式：10点10分10秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string FormatTime02(float seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(seconds));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString("00") + "时 " + ts.Minutes.ToString("00") + "分 " + ts.Seconds.ToString("00")+"秒";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString("00") + "分 " + ts.Seconds.ToString("00")+"秒";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = "00分 " + ts.Seconds.ToString("00")+"秒";
            }
            return str;
        }
    }
}
