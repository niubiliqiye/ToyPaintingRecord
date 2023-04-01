using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseShop : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool isExit;

    private void Update()
    {
        if (isExit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ShopManager.Instatic.theCurrentlySelectedItemCell != null)
                {
                    ShopManager.Instatic.theCurrentlySelectedItemCell.pitch.gameObject.SetActive(false);
                }
                ShopManager.Instatic.tooltip.itemNameText.text = null;
                ShopManager.Instatic.tooltip.itemInfoText.text = null;
                gameObject.SetActive(false);
                //ShopManager.Instatic.npc.npcUI.isUIOpen = false;
                ShopManager.Instatic.npc.gameObject.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                //isExit = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isExit = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isExit = false;
    }
}
