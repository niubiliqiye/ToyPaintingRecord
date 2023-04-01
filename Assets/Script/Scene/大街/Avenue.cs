using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Avenue : MonoBehaviour
{
    private GameObject bulletinBoard;

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
}
