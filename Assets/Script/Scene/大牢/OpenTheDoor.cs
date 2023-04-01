using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    private bool isEnter;

    private Prison prison;

    private new Collider2D collider2D;

    public ItemData_SO key;

    public GameObject tip;

    private TextMeshProUGUI tipText;
    private bool isMouseDown;

    private void Awake()
    {
        prison = FindObjectOfType<Prison>();
        collider2D = gameObject.GetComponent<Collider2D>();
        tipText = tip.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isEnter && isMouseDown)
        {
            if (InventoryManager.Instatic.inventoryData.FindItem<bool>(key))
            {
                isMouseDown = false;
                isEnter = false;
                prison.isOpenTheDoor = true;
                collider2D.enabled = false;
            }
            else
            {
                isMouseDown = false;
                tipText.text = "线索不足";
                tip.SetActive(true);
                Invoke("CloseTip",1f);
            }

        }
    }

    private void CloseTip()
    {
        tip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isEnter = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isEnter = false;
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }
}
