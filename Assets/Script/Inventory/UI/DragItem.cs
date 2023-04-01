using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ItemUI currentItemUI;

    /// <summary>
    /// 当前格子
    /// </summary>
    private SlotHolder currentHolder;

    /// <summary>
    /// 目标格子
    /// </summary>
    private SlotHolder targetHolder;

    private void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }

    /// <summary>
    /// 纪录原始数据
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instatic.currentDrag = new InventoryManager.DragData();
        InventoryManager.Instatic.currentDrag.originalHolder = GetComponent<SlotHolder>();
        InventoryManager.Instatic.currentDrag.originalParent = (RectTransform)transform.parent;
        transform.SetParent(InventoryManager.Instatic.dragCanvas.transform, false);
    }

    /// <summary>
    /// 更随鼠标位置移动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    /// <summary>
    /// 放下物品  交换数据
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        print(5);
        //是否指向UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            print(4);
            if (/*InventoryManager.Instatic.CheckInActionyUI(eventData.position) ||*/
                InventoryManager.Instatic.CheckInInventoryUI(eventData.position))
            {
                print(3);
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                }
                else
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }

                //Debug.Log(eventData.pointerEnter.gameObject);

                //判断是否目标holder是否是我的原holder
                if (targetHolder != InventoryManager.Instatic.currentDrag.originalHolder)
                {
                    switch (targetHolder.slotType)
                    {
                        case SlotType.BAG:
                            SwapItem();
                            break;
                        case SlotType.ACTION:
                            if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Prop)
                                SwapItem();
                            break;
                    }
                }

                currentHolder.UpdateItem();
                targetHolder.UpdateItem();
            }
        }

        transform.SetParent(InventoryManager.Instatic.currentDrag.originalParent);
        
        RectTransform t = transform as RectTransform;
        
        //让格子回到初始位置
        t.offsetMax = -Vector2.zero * 5;
        t.offsetMin=Vector2.zero*5;
    }

    public void SwapItem()
    {
        //目标物品
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];
        //需要交换的物品
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];

        //判断拖拽物品和目标物品是否相同
        bool isSameTiem = tempItem.itemData == targetItem.itemData;

        if (isSameTiem && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index] = tempItem;
        }
    }
}