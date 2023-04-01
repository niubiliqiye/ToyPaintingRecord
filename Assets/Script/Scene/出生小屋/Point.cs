using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject nextScene;
    [Header("画卷")]public ItemData_SO pictureScroll;
    public GameObject dialogueUI;
    public GameObject indoorUI;
    /// <summary>
    /// 新手引导图片
    /// </summary>
    [Header("新手引导图片")]public GameObject[] beginnerGuidanceImage;

    private int beginnerGuidanceIndex=0;
    
    

    private void Start()
    {
        if (!GameManager.Instatic.oneSceneBeginnerGuidanceAccomplish)
        {
            BeginnerGuidance(beginnerGuidanceIndex);
        }
        else
        {
            indoorUI.SetActive(true);
            UpdateDialogueUI();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&beginnerGuidanceIndex<6&&!GameManager.Instatic.oneSceneBeginnerGuidanceAccomplish)
        {
            NextBeginnerGuidanceImage();
        }
        if (!nextScene.activeSelf)
        {
            if (InventoryManager.Instatic.inventoryData.FindItem<bool>(pictureScroll))
            {
                nextScene.SetActive(true);
            }
        }
    }

    private void UpdateDialogueUI()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(DialogueUI.Instatic.currentData);
        DialogueUI.Instatic.UpdateMainDialogue(DialogueUI.Instatic.currentData.dialoguePieces[0]);
    }

    private void BeginnerGuidance(int index)
    {
        GameManager.Instatic.ForbidControl();
        if (index!=0)
        {
            beginnerGuidanceImage[index - 1].SetActive(false);
        }
        
        beginnerGuidanceImage[index].SetActive(true);
    }

    private void NextBeginnerGuidanceImage()
    {
        beginnerGuidanceIndex++;
        if (beginnerGuidanceIndex < 6)
        {
            BeginnerGuidance(beginnerGuidanceIndex);
        }
        else
        {
            beginnerGuidanceImage[beginnerGuidanceIndex-1].SetActive(false);
            indoorUI.SetActive(true);
            UpdateDialogueUI();
        }
    }
}
