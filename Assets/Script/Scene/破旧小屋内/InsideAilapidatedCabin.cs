using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsideAilapidatedCabin : MonoBehaviour
{
    /// <summary>
    /// 黑色提示UI
    /// </summary>
    [Header("黑色提示UI")]public GameObject blackTip;
    
    /// <summary>
    /// 对话系统
    /// </summary>
    [Header("对话系统")]public GameObject dialogueUI;
    
    /// <summary>
    /// 对话数据
    /// </summary>
    [Header("对话数据")]public DialogueData_SO dialogueDataSo;

    /// <summary>
    /// 场景跳转
    /// </summary>
    [Header("场景跳转")] public GameObject nextScene;
    

    private void Start()
    {
        if (GameManager.Instatic.theFirstTimeEnteredTheShabbyHut)
        {
            GameManager.Instatic.theFirstTimeEnteredTheShabbyHut = false;
            blackTip.GetComponent<TimedShutdown>().lengthOfTime = 4f;
            blackTip.GetComponentInChildren<TextMeshProUGUI>().text="";
            blackTip.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 4f);
            Invoke("OpenDialogue",5f);
            Invoke("OpenNextScene",6f);
            Money.Instatic.ingotNumber = 0;
            Money.Instatic.copperCoinNumber=30;
        }
        else
        {
            blackTip.SetActive(false);
            nextScene.SetActive(true);
        }
    }
    
    private  void OpenDialogue()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(dialogueDataSo);
        DialogueUI.Instatic.UpdateMainDialogue(dialogueDataSo.dialoguePieces[0]);
    }

    private void OpenNextScene()
    {
        nextScene.SetActive(true);
    }
}
