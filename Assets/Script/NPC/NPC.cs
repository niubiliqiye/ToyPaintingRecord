using System;
using System.IO;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class Npc : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("对话文件")] public DialogueData_SO dialogFile;

        [FormerlySerializedAs("_npcName")] [Header("NPC名字")]
        public string npcName;

        [Header("交互界面")] public NPCUI npcUI;

        //[Header("对话框")] public GameObject dialogBox;

        [HideInInspector][Header("进入的物体")] public GameObject enterBox;

        [HideInInspector][Header("是否进入")] public bool isEnter;

        /// <summary>
        /// 是否为商人
        /// </summary>
        [Header("是否为商人")]public bool isMerchant;

        /// <summary>
        /// 商店列表数据
        /// </summary>
        public InventoryData_SO shopData;

        public ItemData_SO DeleteIten;

        protected GameObject player;
        protected float distance;
        public float minDistance;
        

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// 交互判断
        /// </summary>
        /// <param name="obj">进入触发器的物体</param>
        /// <returns></returns>
        public virtual bool Interactive(GameObject obj)
        {
            if (obj.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 赋值操作
        /// </summary>
        /// <param name="obj">对话框物体</param>
        // public virtual void Assignment(GameObject obj)
        // {
        //     dialogBox.GetComponent<DialogSystem>().npcName = npcName;
        //     dialogBox.GetComponent<DialogSystem>().playerName = obj.GetComponent<Player>().playerName;
        //     dialogBox.GetComponent<DialogSystem>().textFile = dialogFile;
        //     dialogBox.SetActive(true);
        // }
    }
}