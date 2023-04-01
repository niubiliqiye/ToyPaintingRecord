using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class CameraManager : MonoBehaviour
    {
        /// <summary>
        /// 创建单例模式
        /// </summary>
        public static CameraManager Instance;

        /// <summary>
        /// 判断鼠标是否移入图标
        /// </summary>
        [FormerlySerializedAs("is_enter")] public bool isEnter;
        
        /// <summary>
        /// 按钮是否按下
        /// </summary>
        public bool isPress;

        /// <summary>
        /// 动画控制机
        /// </summary>
        private Animator _anim;

        /// <summary>
        /// 动画控制机状态信息
        /// </summary>
        private AnimatorStateInfo _animStateInfo;

        /// <summary>
        /// 物体标签名称
        /// </summary>
        [FormerlySerializedAs("Tag_name")] public string tagName;

        [Header("UI图标")] [FormerlySerializedAs("UI")]
        public GameObject[] ui;

        /// <summary>
        /// 是否开始游戏  true:开始游戏   false:未开始游戏
        /// </summary>
        //private bool _isStartGame;

        private LoadManager loadManager;
        
        
        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
            //获取动画控制机
            _anim = GetComponent<Animator>();
            loadManager = FindObjectOfType<LoadManager>();
        }


        // Update is called once per frame
        void Update()
        {
            //if (!_isStartGame)
            {
                if (isEnter)
                {
                    _anim.SetBool("isEnter", true);
                }
                else
                {
                    _anim.SetBool("isEnter", false);
                }
            }
            if (isPress)
            {
                print(1);
                if (tagName == "始")
                {
                    foreach (var u in ui)
                    {
                        u.SetActive(false);
                    }
                    _anim.SetBool("Play", true);
                    loadManager.sceneIndex = 1;
                    isPress = false;
                    //_isStartGame = true;
                }
                print(1);
                if (tagName == "续")
                {
                    foreach (var u in ui)
                    {
                        u.SetActive(false);
                    }
                    _anim.SetBool("Play", true);
                    isPress = false;
                    //TODO:将继续场景转换补全
                }
                print(1);
                if (tagName == "退")
                {
                    isPress = false;
                    Application.Quit();
                }
                print(1);
                if (tagName == "选")
                {
                    print(1);
                    foreach (var u in ui)
                    {
                        u.SetActive(false);
                    }
                    _anim.SetBool("Play", true);
                    isPress = false;
                    loadManager.sceneIndex = 3;
                }
            }

            /*if (_isStartGame)
            {
                if (_animStateInfo.IsName("begin") && _animStateInfo.normalizedTime >= 0.99f)
                {
                    Debug.Log("开始游戏");
                    loadManager.LoadNextLevel();
                }
            }*/
        }

        public void SatrtGame()
        {
            loadManager.LoadNextLevel();
        }
    }
}