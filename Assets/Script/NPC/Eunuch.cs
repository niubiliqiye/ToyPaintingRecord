using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Script;
using Spine.Unity;
using UnityEngine;

public class Eunuch : Npc
{
    private DialogueController dialogueController;
    public GameObject beginnerGuidance;

    private void Awake()
    {
        dialogueController = gameObject.GetComponent<DialogueController>();
    }

    void Update()
    {
        if (isEnter)
        {
            if (Input.GetKey(KeyCode.F)&&GameManager.Instatic.npcDialogueBeginnerGuidanceAccomplish)
            {
                dialogueController.OpenDialogue();
                isEnter = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instatic.npcDialogueBeginnerGuidanceAccomplish)
        {
            beginnerGuidance.SetActive(true);
            GameManager.Instatic.ForbidControl();
        }
        enterBox = other.gameObject;
        isEnter = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enterBox = null;
        isEnter = false;
    }

    private void OnMouseDown()
    {
        if (beginnerGuidance.activeSelf)
        {
            beginnerGuidance.SetActive(false);
            GameManager.Instatic.npcDialogueBeginnerGuidanceAccomplish = true;
            GameManager.Instatic.AllowControl();
        }
    }
}
