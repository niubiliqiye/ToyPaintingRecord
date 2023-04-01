using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    //private ShopManager shopManager;

    //private InventoryManager inventoryManager;

    private void Awake()
    {
        //shopManager = FindObjectOfType<ShopManager>();
    }

    public void PurchaseItem()
    {
        if (ShopManager.Instatic.theCurrentlySelectedItem!=null)
        {
                    if (Money.Instatic.CompareMoney(ShopManager.Instatic.theCurrentlySelectedItem))
                    {
                        Money.Instatic.PurchaseItem(ShopManager.Instatic.theCurrentlySelectedItem);
                        DestroyItem();
                    }
        }
    }

    private void DestroyItem()
    {
        foreach (var item in ShopManager.Instatic.shopData.items)
        {
            if (item.itemData == ShopManager.Instatic.theCurrentlySelectedItem)
            {
                if (item.amount > 1)
                {
                    item.amount -= 1;
                    break;
                }
                else
                {
                    item.itemData = null;
                    item.amount = 0;
                    ShopManager.Instatic.theCurrentlySelectedItemCell.pitch.gameObject.SetActive(false);
                    ShopManager.Instatic.tooltip.itemNameText.text = null;
                    ShopManager.Instatic.tooltip.itemInfoText.text = null;
                    break;
                }
            }
        }
        ShopManager.Instatic.shopUI.RefreshUI();
    }
}
