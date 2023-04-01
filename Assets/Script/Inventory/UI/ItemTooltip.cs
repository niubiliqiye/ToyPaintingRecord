using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [Header("提示UI面板")]public GameObject tipPanel;

    [Header("放大提示UI面板")] public GameObject itemEnlargeDetailUI;
    
    [Header("物品名字UI")] public TextMeshProUGUI itemNameText;

    [Header("物品简介UI")] public TextMeshProUGUI itemInfoText;

    [Header("是否改变位置")] public bool changePosition = true;

    private RectTransform rectTransform;

    private void Awake()
    {
        /*if (tipPanel == null)
        {
            itemNameText = tipPanel.GetComponentInChildren<TextMeshProUGUI>();
        }*/
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupTooltip(ItemData_SO item)
    {
        itemNameText.text = item.itemName;
        itemInfoText.text = item.description;
    }

    public void PickUpTooltip(ItemData_SO item)
    {
        itemNameText.text = "获得  "+"<color=#CD3D3D>"+item.itemName+"</color>";
        Invoke("CloseToolTip",2f);
    }

    // private void OnEnable()
    // {
    //     if (changePosition)
    //         UpdataPosition();
    // }

    private void CloseToolTip()
    {
        tipPanel.SetActive(false);
    }
    
    private void Update()
    {
        if (changePosition)
            UpdataPosition();
    }

    public void UpdataPosition()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;

        if (mousePos.y < height)
        {
            rectTransform.position = mousePos + Vector3.up * height * 0.6f;
        }

        else if (Screen.width - mousePos.x > width)
        {
            rectTransform.position = mousePos + Vector3.right * width * 0.6f;
        }

        else
        {
            rectTransform.position = mousePos + Vector3.left * width * 0.6f;
        }
    }
}