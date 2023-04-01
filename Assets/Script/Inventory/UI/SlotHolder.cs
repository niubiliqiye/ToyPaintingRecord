using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SlotType{BAG,ACTION,Shop}
public class SlotHolder : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [Header("格子的类型")]
    public SlotType slotType;
    
    [Header("格子里的UI")]
    public ItemUI itemUI;
    
    /// <summary>
    /// 物品选中状态
    /// </summary>
    [Header("物品选中状态")] public Image pitch;

    public void UpdateItem()
    {
        switch (slotType)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instatic.inventoryData;
                break;
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instatic.actionData;
                break;
            case SlotType.Shop:
                itemUI.Bag = ShopManager.Instatic.shopData;
                break;
                
        }
        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetItemUI(item.itemData,item.amount);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemUI.Getitem())
        {
            if (slotType==SlotType.Shop)
            {
                if (ShopManager.Instatic.theCurrentlySelectedItemCell != null)
                {
                    ShopManager.Instatic.theCurrentlySelectedItemCell.pitch.gameObject.SetActive(false);
                    ShopManager.Instatic.tooltip.itemNameText.text = null;
                    ShopManager.Instatic.tooltip.itemInfoText.text = null;
                }
                pitch.gameObject.SetActive(true);
                ShopManager.Instatic.theCurrentlySelectedItem = itemUI.Getitem();
                ShopManager.Instatic.theCurrentlySelectedItemCell = gameObject.GetComponent<SlotHolder>();
                ShopManager.Instatic.tooltip.SetupTooltip(itemUI.Getitem());
                ShopManager.Instatic.ingotPrice.text = itemUI.Getitem().ingotPrice.ToString();
                ShopManager.Instatic.copperCoinPrice.text = itemUI.Getitem().copperCoinPrice.ToString();
                //InventoryManager.Instatic.tooltip.gameObject.SetActive(true);
            }
            else
            {
                InventoryManager.Instatic.OpenItemEnlargeDetailUI(itemUI.Getitem());
                InventoryManager.Instatic.onclickItemDataSo = itemUI.Getitem();
                GameManager.Instatic.itemTipDialogue = true;
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotType!=SlotType.Shop)
        {
            if (itemUI.Getitem())
            {
                pitch.gameObject.SetActive(true);
                InventoryManager.Instatic.tooltip.SetupTooltip(itemUI.Getitem());
                //InventoryManager.Instatic.tooltip.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (slotType!=SlotType.Shop)
        {
            pitch.gameObject.SetActive(false);
            InventoryManager.Instatic.tooltip.itemNameText.text = null;
            InventoryManager.Instatic.tooltip.itemInfoText.text = null;
            //InventoryManager.Instatic.tooltip.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //InventoryManager.Instatic.tooltip.gameObject.SetActive(false);
    }
}
