using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QianMaster : Npc
{
    public GameObject dialogueUI;
    public GameObject blackTip;
    
    /// <summary>
    /// 第二段对话
    /// </summary>
    [Header("第二段对话")]public DialogueData_SO theSecondDialogue;
    
    /// <summary>
    /// 第三段对话
    /// </summary>
    [Header("第三段对话")]public DialogueData_SO theThirdDialogue;

    /// <summary>
    /// 第一段对话完成
    /// </summary>
    [HideInInspector]public bool theFirstParagraphIsFinished;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (!GameManager.Instatic.firstTimeToTalkToQianMaste)
        {
            gameObject.GetComponent<DialogueController>().currentData = theThirdDialogue;
        }

        if (theFirstParagraphIsFinished)
        {
            theFirstParagraphIsFinished = false;
            blackTip.GetComponentInChildren<TextMeshProUGUI>().text = "过了一会";
            blackTip.SetActive(true);
            blackTip.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 3f);
            Invoke("CloseBlackTip",5f);
        }
    }
    
    private void OnMouseDown()
    {
        if (distance<minDistance&&!dialogueUI.activeSelf)
        {
            if (!npcUI.isUIOpen&&!dialogueUI.activeSelf)
            {
                npcUI.OpenAnimation();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (isMerchant&&!dialogueUI.activeSelf)
        {
            ShopManager.Instatic.shopData = shopData;
            ShopManager.Instatic.npc = gameObject.GetComponent<Jeweler>();
        }
        enterBox = player.gameObject;
        isEnter = true;
    }

    private void CloseBlackTip()
    {
        blackTip.GetComponentInChildren<TextMeshProUGUI>().text = "";
        blackTip.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 3f);
        Invoke("OpenSecondDialogue", 4f);
    }

    /// <summary>
    /// 打开第二段对话
    /// </summary>
    private void OpenSecondDialogue()
    {
        GetComponent<SkeletonAnimation>().AnimationName = "huida";
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(theSecondDialogue);
        DialogueUI.Instatic.UpdateMainDialogue(theSecondDialogue.dialoguePieces[0]);
    }
}