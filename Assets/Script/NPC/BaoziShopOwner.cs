using System.Collections;
using System.Collections.Generic;
using Script;
using Spine.Unity;
using UnityEngine;

public class BaoziShopOwner : Npc
{
    public GameObject dialogueUI;

    /// <summary>
    /// 第二段对话
    /// </summary>
    [Header("第二段对话")]public DialogueData_SO theSecondDialogue;
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
        if (distance<minDistance)
        {
            // if (!npcUI.isUIOpen)
            // {
            //     if (isMerchant)
            //     {
            //         ShopManager.Instatic.shopPanel.SetActive(!false);
            //         ShopManager.Instatic.shopUI.RefreshUI();
            //         ShopManager.Instatic.Refresh();
            //         gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
            //         npcUI.isUIOpen = true;
            //     }
            //     else
            //     {
            //         npcUI.OpenAnimation();
            //     }
            // }

            if (!dialogueUI.activeSelf&&Input.GetMouseButtonDown(0)&&!GameManager.Instatic.completeTheFirstConversationWithTheOwnerOfTheDumplingShop&&!ShopManager.Instatic.shopPanel.activeSelf)
            {
                GameManager.Instatic.ForbidControl();
                //打开UI面板
                dialogueUI.SetActive(true);
                //传输对话内容信息
                DialogueUI.Instatic.UpdateDialogueData(dialogFile);
                DialogueUI.Instatic.UpdateMainDialogue(dialogFile.dialoguePieces[0]);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
            }

            if (!dialogueUI.activeSelf&&Input.GetMouseButtonDown(0)&&GameManager.Instatic.completeTheFirstConversationWithTheOwnerOfTheDumplingShop&&!ShopManager.Instatic.shopPanel.activeSelf)
            {
                GameManager.Instatic.ForbidControl();
                //打开UI面板
                dialogueUI.SetActive(true);
                //传输对话内容信息
                DialogueUI.Instatic.UpdateDialogueData(theSecondDialogue);
                DialogueUI.Instatic.UpdateMainDialogue(theSecondDialogue.dialoguePieces[0]);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
            }
        }
    }

    private void OnMouseEnter()
    {
        if (isMerchant&&!dialogueUI.activeSelf)
        {
            ShopManager.Instatic.shopData = shopData;
            ShopManager.Instatic.npc = gameObject.GetComponent<BaoziShopOwner>();
        }
        enterBox = player.gameObject;
        isEnter = true;

    }
}
