using System;
using Spine.Unity;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Script
{
    public class TestNpc : Npc
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
            if (distance<minDistance/*&&!GameManager.Instatic.enterTheStreetForTheFirstTime*/)
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
                ShopManager.Instatic.npc = gameObject.GetComponent<TestNpc>();
                
            }
            enterBox = player.gameObject;
            isEnter = true;

        }

        // private void OnMouseExit()
        // {
        //     if (isMerchant)
        //     {
        //         ShopManager.Instatic.shopData = null;
        //         ShopManager.Instatic.testNpc = null;
        //     }
        //     enterBox = null;
        //     isEnter = false;
        // }
    }
}