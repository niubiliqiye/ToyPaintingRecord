using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lock : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public KeyCylinder[] keyCylinders;
    public int[] indexs;
    public ItemData_SO itemDataSo;
    private bool isMouseEnter;
    private LoadManager loadManager;
    public GameObject bleakTip;

    private void Awake()
    {
        keyCylinders = GetComponentsInChildren<KeyCylinder>();
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void Update()
    {
        if (!isMouseEnter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                GameManager.Instatic.AllowControl();
            }
        }
    }

    public void UnLock()
    {
        for (int i = 0; i < indexs.Length; i++)
        {
            if (keyCylinders[i].index!=indexs[i])
            {
                return;
            }

            if (i == indexs.Length - 1 && keyCylinders[i].index == indexs[i])
            {
                //TODO:解锁成功逻辑
                InventoryManager.Instatic.inventoryData.DeleteItem(itemDataSo);
                transform.parent.gameObject.SetActive(false);
                bleakTip.GetComponentInChildren<TextMeshProUGUI>().text = "遇河打开了门锁";
                bleakTip.SetActive(true);
                Invoke("NextScene",3f);
                //Debug.LogWarning("解锁成功");
            }
        }
    }

    // private void OnMouseEnter()
    // {
    //     isMouseEnter = true;
    // }
    //
    // private void OnMouseOver()
    // {
    //     if (!isMouseEnter)
    //     {
    //         isMouseEnter = true;
    //     }
    // }
    //
    // private void OnMouseExit()
    // {
    //     isMouseEnter = false;
    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEnter = false;
    }

    private void NextScene()
    {
        loadManager.sceneIndex = 5;
        loadManager.LoadNextLevel();
    }
}
