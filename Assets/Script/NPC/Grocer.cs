using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Grocer : Npc
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
            if (Input.GetMouseButtonDown(0)&&!npcUI.isUIOpen&&!ShopManager.Instatic.shopUI.gameObject.activeSelf&&!dialogueUI.activeSelf)
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
            ShopManager.Instatic.npc = gameObject.GetComponent<Grocer>();
        }
        enterBox = player.gameObject;
        isEnter = true;
    }
}
