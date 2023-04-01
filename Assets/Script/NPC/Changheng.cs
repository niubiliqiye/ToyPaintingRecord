using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Changheng : Npc
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = gameObject.GetComponent<DialogueController>();
    }
    void Update()
    {
        if (isEnter)
        {
            if (Input.GetKey(KeyCode.F))
            {
                dialogueController.OpenDialogue();
                isEnter = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enterBox = other.gameObject;
        isEnter = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enterBox = null;
        isEnter = false;
    }
}
