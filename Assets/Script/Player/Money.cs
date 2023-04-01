using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class Money : Singleton<Money>
{
    /// <summary>
    /// 元宝数量
    /// </summary>
    [Header("元宝价格")] public int ingotNumber;
        
    /// <summary>
    /// 铜钱数量
    /// </summary>
    [Header("铜钱价格")] public int copperCoinNumber;


    private int totalMoney;

    /// <summary>
    /// 对比钱款
    /// </summary>
    /// <param name="item">需购买的物品</param>
    /// <returns></returns>
    public bool CompareMoney(ItemData_SO item)
    {
        totalMoney = ingotNumber * 1000 + copperCoinNumber;
        var itemTotalPrice = item.ingotPrice * 1000 + item.copperCoinPrice;
        if (itemTotalPrice<=totalMoney)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="item">购买的物品</param>
    public void PurchaseItem(ItemData_SO item)
    {
        totalMoney-=(item.ingotPrice * 1000 + item.copperCoinPrice);
        Conversion(totalMoney);
        InventoryManager.Instatic.tooltip02.PickUpTooltip(ShopManager.Instatic.theCurrentlySelectedItem);
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
        Invoke("CloseTooltip",1f);
        InventoryManager.Instatic.inventoryData.AddItem(ShopManager.Instatic.theCurrentlySelectedItem, ShopManager.Instatic.theCurrentlySelectedItem.itemAmount);
        InventoryManager.Instatic.inventoryUI.RefreshUI();
    }

    private void Conversion(int totalMoney)
    {
        ingotNumber = totalMoney / 1000;
        copperCoinNumber = totalMoney % 1000;
        ShopManager.Instatic.Refresh();
    }

    private void CloseTooltip()
    {
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
    }
}
