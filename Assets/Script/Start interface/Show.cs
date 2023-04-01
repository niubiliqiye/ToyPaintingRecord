using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class Show : MonoBehaviour
    {
        [FormerlySerializedAs("Image01")] public GameObject image01;
        [FormerlySerializedAs("Image02")] public GameObject image02;
        [FormerlySerializedAs("Image03")] public GameObject image03;

        [FormerlySerializedAs("Image04")] public GameObject image04;
        public GameObject image05;

        /// <summary>
        /// 动画控制机
        /// </summary>
        private Animator _anim;

        /// <summary>
        /// 动画控制机状态信息
        /// </summary>
        AnimatorStateInfo _animStateInfo;

        /// <summary>
        /// 动画控制机parameter名称
        /// </summary>
        private string _animName;

        /// <summary>
        /// 动画控制机层级
        /// </summary>
        private string _stateinfoName;

        // Start is called before the first frame update
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.tag==CameraManager.Instance.tagName)
            {
                if (_animName == null && _stateinfoName == null)
                {
                    if (CameraManager.Instance.tagName == "始")
                    {
                        _animName = "is_play";
                        _stateinfoName = "Close1";
                    }
                    else if (CameraManager.Instance.tagName == "续")
                    {
                        _animName = "is_play2";
                        _stateinfoName = "Close2";
                    }
                    else if (CameraManager.Instance.tagName == "退")
                    {
                        _animName = "is_play3";
                        _stateinfoName = "Close3";
                    }
                    else if (CameraManager.Instance.tagName == "选")
                    {
                        _animName = "is_play4";
                        _stateinfoName = "Close4";
                    }
                }
                _animStateInfo = _anim.GetCurrentAnimatorStateInfo(0);
                if (CameraManager.Instance.isEnter)
                {
                    image01.SetActive(true);
                    image02.SetActive(true);
                    image03.SetActive(true);
                    image04.SetActive(true);
                    image05.SetActive(true);
                    _anim.SetBool(_animName, true);
                }
                else
                {
                    _anim.SetBool(_animName, false);
                    if (_animStateInfo.IsName(_stateinfoName) && _animStateInfo.normalizedTime>=0.99f)
                    {
                        image01.SetActive(false);
                        image02.SetActive(false);
                        image03.SetActive(false);
                        image04.SetActive(false);
                        image05.SetActive(false);
                        _animName = null;
                        _stateinfoName = null;
                        CameraManager.Instance.tagName = null;
                    }
                }
            }
        }
    }
}