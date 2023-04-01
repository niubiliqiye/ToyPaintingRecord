using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{
    /// <summary>
    /// 存放背包数据的列表
    /// </summary>
    public List<Inventory> items = new List<Inventory>();

    /// <summary>
    /// 将物品添加进背包
    /// </summary>
    /// <param name="newItemData">新物品的数据信息</param>
    /// <param name="amount">新物品的数量</param>
    public void AddItem(ItemData_SO newItemData, int amount)
    {
        //判断当前背包是否有该物品
        bool found = false;
        
        if (newItemData.stackable)
        {
            //背包中寻找相同物品
            foreach (var item in items)
            {
                if (item.itemData == newItemData)
                {
                    item.amount += amount;
                    found = true;
                    break;
                }
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            //寻找背包中最近的空位（在背包中没有相同的物品情况下）
            if (items[i].itemData == null && !found)
            {
                items[i].itemData = newItemData;
                items[i].amount = amount;
                break;
            }
        }
    }

    /// <summary>
    /// 将物品从背包中删除
    /// </summary>
    /// <param name="newItemData">需要删除的数据</param>
    public void DeleteItem(ItemData_SO newItemData)
    {
        //背包中寻找相同物品
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == newItemData)
            {
                if (items[i].amount>1)
                {
                    items[i].amount --;
                    break;
                }
                if(items[i].amount==1)
                {
                    items[i].itemData =null;
                    items[i].amount = 0;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 在背包中查找物体
    /// </summary>
    /// <param name="newItemData"></param>
    /// <returns></returns>
    public bool FindItem(ItemData_SO newItemData)
    {
        foreach (var item in items)
        {
            if (item.itemData == newItemData)
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// 在背包中查找物体的数量
    /// </summary>
    /// <param name="newItemData"></param>
    /// <returns></returns>
    public int FindItemQuantity(ItemData_SO newItemData)
    {
        foreach (var item in items)
        {
            if (item.itemData == newItemData)
            {
                return item.amount;
            }
        }
        return 0;
    }
}

[System.Serializable] //序列化
public class Inventory
{
    public ItemData_SO itemData;

    /// <summary>
    /// 物品数量
    /// </summary>
    public int amount;
}