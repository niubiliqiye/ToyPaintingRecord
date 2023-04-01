using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("物品图片UI")]public Image icon = null;
    
    [Header("物品数量UI")]public TextMeshProUGUI amount = null;

    /// <summary>
    /// 背包数据
    /// </summary>
    public InventoryData_SO Bag { get; set; }

    /// <summary>
    /// 数据存放编号
    /// </summary>
    public int Index { get; set; } = -1;

    public void SetItemUI(ItemData_SO item, int ItemAmount)
    {
        if (ItemAmount == 0)
        {
            Bag.items[Index].itemData = null;
            icon.gameObject.SetActive(false);
            return;
        }
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            amount.text = ItemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }

    public ItemData_SO Getitem()
    {
        return Bag.items[Index].itemData;
    }
}
