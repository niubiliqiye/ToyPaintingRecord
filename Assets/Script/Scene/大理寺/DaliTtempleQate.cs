using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DaliTtempleQate : MonoBehaviour
{
    private bool isEnter;
    private bool isMouseDown;

    /// <summary>
    /// 是否调查身世
    /// </summary>
    private bool isInvestigateOneOrigin;

    public GameObject nextScene;

    public GameObject tip;
    private TextMeshProUGUI tipText;
    private LoadManager loadManager;
    public GameObject dialogueUI;

    private void Awake()
    {
        tipText = tip.GetComponentInChildren<TextMeshProUGUI>();
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
        if (isEnter && isMouseDown)
        {
            isEnter = false;
            isMouseDown = false;
            loadManager.sceneIndex = 6;
            loadManager.LoadNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isInvestigateOneOrigin)
        {
            tipText.color=Color.black;
            tipText.text = "点击进入大理寺";
            tip.SetActive(true);
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tip.SetActive(false);
        isEnter = false;
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    // private void CloseTip()
    // {
    //     tip.SetActive(false);
    // }

    private void InvestigateOneOrigin()
    {
        isInvestigateOneOrigin = true;
        nextScene.SetActive(false);
    }

    private void ExitScene()
    {
        isInvestigateOneOrigin = false;
        nextScene.SetActive(true);
    }
}
