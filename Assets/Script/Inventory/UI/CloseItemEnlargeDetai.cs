using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class CloseItemEnlargeDetai : MonoBehaviour
{
    public GameObject itemEnlargeDetaiUI;

    public BoxCollider2D boxcollider2D;

    private Sprite itemEnlargeDetaiImage;

    private bool mouseIsOnTrigger;

    public GameObject dialogueUI;

    public DialogueData_SO dialogue;
    private float time;

    private void Start()
    {
        itemEnlargeDetaiImage = gameObject.GetComponent<Image>().sprite;
        boxcollider2D.size = new Vector2(itemEnlargeDetaiImage.bounds.size.x*100, itemEnlargeDetaiImage.bounds.size.y*100);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (!mouseIsOnTrigger&&time>5f)
        {
            print(1);
            if (Input.GetMouseButtonDown(0) && itemEnlargeDetaiUI.activeSelf)
            {
                itemEnlargeDetaiUI.SetActive(false);
                GameManager.Instatic.AllowControl();
                if (!GameManager.Instatic.itemTipDialogue)
                {
                    Invoke("OpenDialogue",2f);
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseIsOnTrigger = true;
    }

    private void OnMouseExit()
    {
        mouseIsOnTrigger = false;
    }
    
    private  void OpenDialogue()
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(dialogue);
        DialogueUI.Instatic.UpdateMainDialogue(dialogue.dialoguePieces[0]);
    }
    
}
