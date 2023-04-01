using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Avenue : MonoBehaviour
{
    private GameObject bulletinBoard;

    public GameObject[] tutorialImage;

    public int tutorialImageIndex;
    
    private void Awake()
    {
        bulletinBoard = FindObjectOfType<BulletinBoard>().gameObject;
    }

    private void Start()
    {
        if (!GameManager.Instatic.completeTheFirstConversationWithTheOwnerOfTheDumplingShop)
        {
            bulletinBoard.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }

    public void NextTutorialImage()
    {
        tutorialImageIndex++;
        if (tutorialImageIndex<tutorialImage.Length)
        {
            tutorialImage[tutorialImageIndex - 1].SetActive(false);
            tutorialImage[tutorialImageIndex].SetActive(true);
        }
        else
        {
            tutorialImage[tutorialImageIndex - 1].SetActive(false);
            GameManager.Instatic.characterInteractiveUITutorialCompleted = true;
        }
    }
}
