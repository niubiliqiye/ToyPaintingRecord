using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Grocer : Npc
{
    public GameObject dialogueUI;
    private Avenue avenue;
    public BoxCollider2D[] boxCollider2Ds;
        
    private void Awake()
    {
        player = GameObject.Find("Player");
        avenue = FindObjectOfType<Avenue>();
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (Input.GetMouseButtonDown(0)&&!GameManager.Instatic.characterInteractiveUITutorialCompleted && avenue.tutorialImageIndex<avenue.tutorialImage.Length && avenue.tutorialImage[avenue.tutorialImageIndex].activeSelf)
        {
            avenue.NextTutorialImage();
        }

        if ((avenue.tutorialImage[0].activeSelf||avenue.tutorialImage[1].activeSelf||avenue.tutorialImage[2].activeSelf)&&boxCollider2Ds[0].enabled)
        {
            for (int i = 0; i < boxCollider2Ds.Length; i++)
            {
                boxCollider2Ds[i].enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (distance<minDistance)
        {
            if (Input.GetMouseButtonDown(0)&&!npcUI.isUIOpen&&!ShopManager.Instatic.shopUI.gameObject.activeSelf&&!dialogueUI.activeSelf)
            {
                npcUI.OpenAnimation();
                if (!GameManager.Instatic.characterInteractiveUITutorialCompleted)
                {
                    Invoke("OpenTheTutorial",0.43f);
                }
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

    /// <summary>
    /// 打开人物UI教程
    /// </summary>
    private void OpenTheTutorial()
    {
        avenue.tutorialImage[0].SetActive(true);
        for (int i = 0; i < boxCollider2Ds.Length; i++)
        {
            boxCollider2Ds[i].enabled = false;
        }
    }
}
