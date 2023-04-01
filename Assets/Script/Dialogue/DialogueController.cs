using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentData;
    public GameObject dialogueUI;

    /// <summary>
    /// 是否可以对话
    /// </summary>
    //private bool canTalk = false;

    private void Awake()
    {
        currentData = gameObject.GetComponent<Npc>().dialogFile;
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.CompareTag("Player")&&currentData!=null)
    //     {
    //         //canTalk = true;
    //     }
    // }

    private void Update()
    {
        // if(canTalk&&Input.GetKeyDown(KeyCode.F))
        // {
        //     OpenDialogue();
        //     //canTalk = false;
        // }
    }

    public void OpenDialogue()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(currentData);
        DialogueUI.Instatic.UpdateMainDialogue(currentData.dialoguePieces[0]);
    }
}
