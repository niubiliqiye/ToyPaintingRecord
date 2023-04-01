using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class OutsideTheShukaku : MonoBehaviour
{
    /// <summary>
    /// 需要的道具
    /// </summary>
    [Header("需要的道具")]public ItemData_SO[] itemDataSo;
    
    /// <summary>
    /// 点击字条
    /// </summary>
    [Header("点击字条")]public bool clickBriefNote;

    /// <summary>
    /// 点击门锁
    /// </summary>
    [Header("点击门锁")]public bool clickDoorLock;

    /// <summary>
    /// 对话系统
    /// </summary>
    [Header("对话系统")]public GameObject dialogueUI;

    /// <summary>
    /// 对话数据
    /// </summary>
    [Header("对话数据")]public DialogueData_SO[] dialogueDataSo;

    /// <summary>
    /// 绘画面板
    /// </summary>
    [Header("绘画面板")] public GameObject drawPanel;

    /// <summary>
    /// 允许绘画
    /// </summary>
    [Header("允许绘画")]public bool permitPainting;

    /// <summary>
    /// 绘画成功
    /// </summary>
    public bool isUnlockingSucceeded;

    /// <summary>
    /// 解锁界面
    /// </summary>
    [Header("解锁界面")]public GameObject lockCanvas;
    
    /// <summary>
    /// 物品提示对话完成
    /// </summary>
    [Header("物品提示对话完成")] public bool itemTipDialogueAccomplish;

    private UI_Animation uiAnimation;

    private void Awake()
    {
        uiAnimation = FindObjectOfType<UI_Animation>();
    }

    private void Update()
    {
        if (clickBriefNote)
        {
            clickBriefNote = false;
            if (!InventoryManager.Instatic.inventoryData.FindItem<bool>(itemDataSo[1]))
            {
                OpenDialogue(1);
            }
            else
            {
                InventoryManager.Instatic.inventoryData.DeleteItem(itemDataSo[1]);
                InventoryManager.Instatic.tooltip02.PickUpTooltip(itemDataSo[0]);
                InventoryManager.Instatic.OpenItemEnlargeDetailUI(itemDataSo[0]);
                InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
                InventoryManager.Instatic.inventoryData.AddItem(itemDataSo[0], 1);
                InventoryManager.Instatic.inventoryUI.RefreshUI();
                Invoke("CloseTooltip",1f);
            }
        }
        
        if (clickDoorLock)
        {
            clickDoorLock=false;
            if (!InventoryManager.Instatic.inventoryData.FindItem<bool>(itemDataSo[0]))
            {
                clickDoorLock=false;
                OpenDialogue(0);
            }
            else if (InventoryManager.Instatic.inventoryData.FindItem<bool>(itemDataSo[0])&&itemTipDialogueAccomplish)
            {
                itemTipDialogueAccomplish = false;
                lockCanvas.SetActive(true);
                GameManager.Instatic.ForbidControl();
            }

        }

        if (permitPainting)
        {
            permitPainting = false;
            drawPanel.SetActive(true);
            GameManager.Instatic.ForbidControl();
        }

        if (isUnlockingSucceeded)
        {
            isUnlockingSucceeded = false;
            InventoryManager.Instatic.tooltip02.PickUpTooltip(itemDataSo[1]);
            InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
            InventoryManager.Instatic.inventoryData.AddItem(itemDataSo[1], 1);
            InventoryManager.Instatic.inventoryUI.RefreshUI();
            Invoke("CloseTooltip",1f);
        }
        
        if (GameManager.Instatic.itemTipDialogue&&!InventoryManager.Instatic.bagPanel.activeSelf&&uiAnimation.skeleton.AnimationState.GetCurrent(0).ToString()=="outside-closeidle")
        {
            GameManager.Instatic.itemTipDialogue = false;
            OpenDialogue(2);
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
    
    private void CloseTooltip()
    {
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
    }
}
