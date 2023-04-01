using System.Collections;
using System.Collections.Generic;
using Script;
using Spine.Unity;
using UnityEngine;

public class BaoziShopOwner : Npc
{
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
            if (!npcUI.isUIOpen)
            {
                if (isMerchant)
                {
                    ShopManager.Instatic.shopPanel.SetActive(!false);
                    ShopManager.Instatic.shopUI.RefreshUI();
                    ShopManager.Instatic.Refresh();
                    gameObject.GetComponent<SkeletonAnimation>().AnimationName = "huida";
                    npcUI.isUIOpen = true;
                }
                else
                {
                    npcUI.OpenAnimation();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (isMerchant)
        {
            ShopManager.Instatic.shopData = shopData;
            ShopManager.Instatic.testNpc = gameObject.GetComponent<TestNpc>();
                
        }
        enterBox = player.gameObject;
        isEnter = true;

    }
}
