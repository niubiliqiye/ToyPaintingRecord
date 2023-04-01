using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BulletinBoard : MonoBehaviour
{
    private float distance;
    private GameObject player;
    public float minDistance;
    public GameObject dialogueUI;
    public DialogueData_SO dialogue;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    private void OnMouseDown()
    {
        if (distance<minDistance&&!dialogueUI.activeSelf&&GameManager.Instatic.completeTheFirstConversationWithTheOwnerOfTheDumplingShop)
        {
            if (!GameManager.Instatic.unlockTheMoneyHouse)
            {
                GameManager.Instatic.unlockTheMoneyHouse = true;
            }
            GameManager.Instatic.ForbidControl();
            //打开UI面板
            dialogueUI.SetActive(true);
            //传输对话内容信息
            DialogueUI.Instatic.UpdateDialogueData(dialogue);
            DialogueUI.Instatic.UpdateMainDialogue(dialogue.dialoguePieces[0]);
        }
    }
}
