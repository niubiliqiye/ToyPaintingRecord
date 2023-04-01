using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [Header("玩家姓名")]public string playerName;
        
        [Header("移动参数")] public float speed;
        
        private SkeletonAnimation skeleton;
        
        /// <summary>
        /// 获取玩家输入的移动方向
        /// </summary>
        private float _xVelocity;
        
        /// <summary>
        /// 获取物体挂载的刚体
        /// </summary>
        private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// 玩家对话的Npc动画
        /// </summary>
        public SkeletonAnimation skeletonAnimationNPC;

        private AudioSource audioSource;

        private void Awake()
        {
            skeleton = GetComponent<SkeletonAnimation>();
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //TODO 人物移动
            if (GameManager.Instatic.isCanBeMove)
            {
                Move();
            }
            else
            {
                skeleton.AnimationName = "idle";
            }
        }

        void Move()
        {
            if (Input.anyKey)
            {
                _xVelocity=Input.GetAxisRaw("Horizontal");//获取玩家输入的移动方向（A为-1、D为1、不安为0）。Input.GetAxisRaw("Horizontal")直接获取-1，0，1。Input.GetAxis("Horizontal")获取的是[-1, 1]，包括范围内的小数值
                if (_xVelocity!=0)
                {
                    skeleton.AnimationName = "walk";
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                }


                _rigidbody2D.velocity = new Vector2(_xVelocity * speed,_rigidbody2D.velocity.y);//给刚体赋值一个速度值
            }
            else
            {
                audioSource.Stop();
                skeleton.AnimationName = "idle";
            }

            //人物转向
            if (_xVelocity != 0)
            {
                if (_xVelocity < 0)
                {
                    gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
            }
        }

        /// <summary>
        /// 纪录与玩家对话的NPC的动画组件
        /// </summary>
        /// <param name="col"></param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.name!="监狱太监")
            skeletonAnimationNPC = col.gameObject.GetComponent<SkeletonAnimation>();
        }
    }
}