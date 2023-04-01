using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/Shop Data")]
public class ShopData_SO : ScriptableObject
{
    /// <summary>
    /// 存放背包数据的列表
    /// </summary>
    public List<Shop> items = new List<Shop>();

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
}

[System.Serializable] //序列化
public class Shop
{
    public ItemData_SO itemData;

    /// <summary>
    /// 物品数量
    /// </summary>
    public int amount;
}
