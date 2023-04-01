using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Avenue : MonoBehaviour
{
    public ItemData_SO baoZi;
    private void Start()
    {
        if (GameManager.Instatic.enterTheStreetForTheFirstTime)
        {
            //GameManager.Instatic.enterTheStreetForTheFirstTime = false;
        }
    }

    private void Update()
    {
        if (GameManager.Instatic.enterTheStreetForTheFirstTime&&InventoryManager.Instatic.actionData.FindItemQuantity(baoZi)==5)
        {
            
        }
    }
}
