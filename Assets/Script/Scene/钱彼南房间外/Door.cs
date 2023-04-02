using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    /// <summary>
    /// 对话系统UI
    /// </summary>
    [Header("对话系统UI")]public GameObject dialogueUI;
    private GameObject player;
    private float distance;

    /// <summary>
    /// 最大距离
    /// </summary>
    [Header("最大距离")]public float maxDistance;

    /// <summary>
    /// 对话文件
    /// </summary>
    [Header("对话文件")] public DialogueData_SO[] dialogFile;
    
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
        if (distance<maxDistance&&!dialogueUI.activeSelf)
        {
            GameManager.Instatic.ForbidControl();
            //打开UI面板
            dialogueUI.SetActive(true);
            //传输对话内容信息
            DialogueUI.Instatic.UpdateDialogueData(dialogFile[0]);
            DialogueUI.Instatic.UpdateMainDialogue(dialogFile[0].dialoguePieces[0]);
        }
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }
}
