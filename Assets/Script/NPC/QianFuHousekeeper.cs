using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Spine.Unity;
using UnityEngine;

public class QianFuHousekeeper : Npc
{
    public GameObject dialogueUI;
    
    /// <summary>
    /// 第二段对话
    /// </summary>
    [Header("第二段对话")]public DialogueData_SO theSecondDialogue;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }
    
    private void OnMouseDown()
    {
        if (distance<minDistance&&!dialogueUI.activeSelf)
        {
            if (GameManager.Instatic.theFirstTimeWithTheMoneyHouseButlerDialogue)
            {
                GameManager.Instatic.ForbidControl();
                //打开UI面板
                dialogueUI.SetActive(true);
                //传输对话内容信息
                DialogueUI.Instatic.UpdateDialogueData(dialogFile);
                DialogueUI.Instatic.UpdateMainDialogue(dialogFile.dialoguePieces[0]);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
            }
            else
            {
                GameManager.Instatic.ForbidControl();
                //打开UI面板
                dialogueUI.SetActive(true);
                //传输对话内容信息
                DialogueUI.Instatic.UpdateDialogueData(theSecondDialogue);
                DialogueUI.Instatic.UpdateMainDialogue(theSecondDialogue.dialoguePieces[0]);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
            }
        }
    }

    private void OnMouseEnter()
    {
        enterBox = player.gameObject;
        isEnter = true;
    }
}
