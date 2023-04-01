using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorOfTheDepartment : MonoBehaviour
{
    public bool isDeleteItem;
    public bool isTheSecondDialogue;

    public GameObject blackTips;
    private LoadManager loadManager;
    public GameObject dialogueUI;
    public GameObject tip;
    public DialogueData_SO[] dialogueDataSos;
    public bool theFirstSegmentIsOver;
    public bool theSecondSegmentIsOver;

    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void Start()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(DialogueUI.Instatic.currentData);
        DialogueUI.Instatic.UpdateMainDialogue(DialogueUI.Instatic.currentData.dialoguePieces[0]);
    }

    private void Update()
    {
        if (isDeleteItem&&!dialogueUI.activeSelf)
        {
            tip.GetComponentInChildren<TextMeshProUGUI>().text = "失去画卷";
            tip.SetActive(true);
            isDeleteItem = false;
            theFirstSegmentIsOver = true;
            Invoke("CloseTip",1f);
            Invoke("OpenDialogUI01",1.5f);
        }

        if (isTheSecondDialogue && !dialogueUI.activeSelf)
        {
            isTheSecondDialogue = false;
            Invoke("OpenDialogUI02",4f);
            Invoke("OpenBlackTips",5f);
        }
    }

    private void OpenBlackTips()
    {
        blackTips.SetActive(true);
        blackTips.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1.5f);
        Invoke("NextScene", 2f);
    }

    private void CloseTip()
    {
        tip.SetActive(false);
    }

    private void NextScene()
    {
        blackTips.SetActive(false);
        loadManager.sceneIndex = 4;
        loadManager.LoadNextLevel();
    }
    
    private void OpenDialogUI01()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[0]);
        DialogueUI.Instatic.UpdateMainDialogue(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[0].dialoguePieces[0]);
    }
    
    private void OpenDialogUI02()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[1]);
        DialogueUI.Instatic.UpdateMainDialogue(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[1].dialoguePieces[0]);
    }
}
