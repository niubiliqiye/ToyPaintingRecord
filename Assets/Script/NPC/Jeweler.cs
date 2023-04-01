using System.Collections;
using System.Collections.Generic;
using Script;
using Spine.Unity;
using UnityEngine;

public class Jeweler : Npc
{
    public GameObject dialogueUI;
        
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
            if (!dialogueUI.activeSelf&&Input.GetMouseButtonDown(0)&&!ShopManager.Instatic.shopPanel.activeSelf)
            {
                GameManager.Instatic.ForbidControl();
                //打开UI面板
                dialogueUI.SetActive(true);
                //传输对话内容信息
                DialogueUI.Instatic.UpdateDialogueData(dialogFile);
                DialogueUI.Instatic.UpdateMainDialogue(dialogFile.dialoguePieces[0]);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";

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
}
