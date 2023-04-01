using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class AvenueDialogueEvent : MonoBehaviour
{
    public GameObject 包子铺老板;
    public GameObject 首饰铺老板;
    public GameObject 杂货铺老板;
    public ItemData_SO 包子;

    public void BuySteamedBun()
    {
        GameManager.Instatic.completeTheFirstConversationWithTheOwnerOfTheDumplingShop = true;
        InventoryManager.Instatic.tooltip02.PickUpTooltip(包子);
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
        ShopManager.Instatic.shopData.DeleteItem(包子);
        InventoryManager.Instatic.inventoryData.AddItem(包子,5);
        Money.Instatic.copperCoinNumber = 0;
        InventoryManager.Instatic.inventoryUI.RefreshUI();
        Invoke("CloseTooltip",1f);
    }

    public void OpenShop()
    {
        GameManager.Instatic.ForbidControl();
        ShopManager.Instatic.shopUI.RefreshUI();
        ShopManager.Instatic.Refresh();
        ShopManager.Instatic.shopPanel.SetActive(true);
    }
    
    private void CloseTooltip()
    {
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
    }
}
