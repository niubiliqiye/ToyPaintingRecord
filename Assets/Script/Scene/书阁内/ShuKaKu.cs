using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class ShuKaKu : MonoBehaviour
{
    /// <summary>
    /// 点击书柜
    /// </summary>
    [Header("点击书柜")]public bool onClickBookCase;

    /// <summary>
    /// 玩家距离书柜距离
    /// </summary>
    [Header("玩家距离书柜距离")] public float distance;
    
    
    /// <summary>
    /// 对话系统
    /// </summary>
    [Header("对话系统")]public GameObject dialogueUI;
    
    /// <summary>
    /// 对话数据
    /// </summary>
    [Header("对话数据")]public DialogueData_SO[] dialogueDataSo;
    
    
    /// <summary>
    /// 打开华容道
    /// </summary>
    [Header("打开华容道")] public bool openHuaRoaD;

    private BookCase[] bookCases;

    /// <summary>
    /// 华容道UI界面
    /// </summary>
    [Header("华容道UI界面")] public GameObject huaRoaDUI;
    

    /// <summary>
    /// 拼图完成
    /// </summary>
    [Header("拼图完成")]public bool jigsawAccomplish;

    /// <summary>
    /// 开始倒计时
    /// </summary>
    [Header("开始倒计时")] public bool startCountDown;

    /// <summary>
    /// 计时结束
    /// </summary>
    [Header("计时结束")] public bool countDownFinish;


    public TestTime testTime;

    /// <summary>
    /// 计时中
    /// </summary>
    [Header("计时中")]public bool inTime;

    /// <summary>
    /// 结局一
    /// </summary>
    [Header("结局一")]public bool outcomeOne;

    /// <summary>
    /// 黑屏提示
    /// </summary>
    [Header("黑屏提示")]public GameObject bleakTip;

    /// <summary>
    /// 找到卷宗
    /// </summary>
    [Header("找到卷宗")]public bool findDossier;

    private LoadManager loadManager;

    public ItemData_SO itemDataSo;

    /// <summary>
    /// 第四段对话结束
    /// </summary>
    [Header("第四段对话结束")]public bool theFourthDialogEnds;

    private UI_Animation uiAnimation;
    

    private void Awake()
    {
        bookCases = FindObjectsOfType<BookCase>();
        loadManager = FindObjectOfType<LoadManager>();
        uiAnimation = FindObjectOfType<UI_Animation>();
    }

    private void Update()
    {
        if (uiAnimation.isOpen)
        {
            if (bookCases[0].gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                for (int i = 0; i < bookCases.Length; i++)
                {
                    bookCases[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        else
        {
            if (!bookCases[0].gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                for (int i = 0; i < bookCases.Length; i++)
                {
                    bookCases[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        
        if (onClickBookCase)
        {
            onClickBookCase = false;
            for (int i = 0; i < bookCases.Length; i++)
            {
                bookCases[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            FindObjectOfType<Player>().gameObject.GetComponent<BoxCollider2D>().enabled = false;
            OpenDialogue(0);
        }

        if (openHuaRoaD)
        {
            openHuaRoaD = false;
            GameManager.Instatic.ForbidControl();
            huaRoaDUI.SetActive(true);
        }

        if (jigsawAccomplish)
        {
            jigsawAccomplish = false;
            huaRoaDUI.SetActive(false);
            GameManager.Instatic.AllowControl();
            OpenDialogue(1);
        }

        if (startCountDown)
        {
            startCountDown = false;
            inTime = true;
            testTime.gameObject.SetActive(true);
        }

        if (countDownFinish)
        {
            countDownFinish = false;
            OpenDialogue(2);
        }

        if (outcomeOne)
        {
            outcomeOne = false;
            bleakTip.GetComponentInChildren<TextMeshProUGUI>().text = "结局一    时不待我";
            bleakTip.SetActive(true);
            Invoke("NextScene",10f);
        }

        if (findDossier)
        {
            findDossier = false;
            testTime.gameObject.SetActive(false);
            InventoryManager.Instatic.inventoryData.DeleteItem(itemDataSo);
            InventoryManager.Instatic.tooltip02.PickUpTooltip(itemDataSo);
            InventoryManager.Instatic.OpenItemEnlargeDetailUI(itemDataSo);
            InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
            InventoryManager.Instatic.inventoryData.AddItem(itemDataSo, 1);
            InventoryManager.Instatic.inventoryUI.RefreshUI();
            Invoke("CloseTooltip",1f);
        }

        if (theFourthDialogEnds)
        {
            theFourthDialogEnds = false;
            bleakTip.GetComponentInChildren<TextMeshProUGUI>().text = "";
            bleakTip.SetActive(true);
            Invoke("NextScene01",2f);
        }
    }
    
    private  void OpenDialogue(int index)
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(dialogueDataSo[index]);
        DialogueUI.Instatic.UpdateMainDialogue(dialogueDataSo[index].dialoguePieces[0]);
    }

    private void NextScene()
    {
        loadManager.sceneIndex = 0;
        loadManager.LoadNextLevel();
        bleakTip.SetActive(false);
    }
    
    private void NextScene01()
    {
        loadManager.sceneIndex = 9;
        loadManager.LoadNextLevel();
        bleakTip.SetActive(false);
    }
    
    private void CloseTooltip()
    {
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
    }
}
