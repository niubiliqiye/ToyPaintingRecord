using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Script.Inventory.Item.ScriptableObject;
using Spine.Unity;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    public class DragData
    {
        [Header("原始格子")] public SlotHolder originalHolder;
        [Header("原始的父级")] public RectTransform originalParent;
    }

    //TODO:最后添加模板用于保存数据

    /// <summary>
    /// 用于保存背包数据
    /// </summary>
    public InventoryData_SO inventoryTemplate;

    [Header("Inventory Data")] [Tooltip("背包列表数据")]
    public InventoryData_SO inventoryData;

    /// <summary>
    /// 用于保存快捷栏数据
    /// </summary>
    public InventoryData_SO actionTemplate;

    [Header("Action Date")] [Tooltip("快捷栏列表数据")]
    public InventoryData_SO actionData;

    [Header("ContainerS")] [Tooltip("背包UI")]
    public ContainerUI inventoryUI;

    [Tooltip("快捷栏UI")] public ContainerUI actionUI;

    [Header("Drag Canvas")] public Canvas dragCanvas;

    public DragData currentDrag;

    private bool isOpen = false;

    [Header("物品信息提示框")] public ItemTooltip tooltip;
    public ItemTooltip tooltip02;

    /// <summary>
    /// 物品放大详情界面
    /// </summary>
    [Header("物品放大详情界面")]public GameObject itemEnlargeDetailUI;

    /*[HideInInspector]*/public Image itemEnlargeDetai;

    [Header("背包面板")] public GameObject bagPanel;

    public SkeletonGraphic skeletonGraphic;

    public GameObject[] beginnerGuidanceImage;

    private int beginnerGuidanceIndex;

    /// <summary>
    /// 查看放大详情的物品
    /// </summary>
    public ItemData_SO onclickItemDataSo;

    /// <summary>
    /// 元宝数量
    /// </summary>
    [Header("元宝数量")]public TextMeshProUGUI ingotNumber;
    
    /// <summary>
    /// 铜钱数量
    /// </summary>
    [Header("铜钱数量")]public TextMeshProUGUI copperCoinNumber;
    
    protected override void Awake()
    {
        base.Awake();
        if (inventoryTemplate != null)
        {
            inventoryData = Instantiate(inventoryTemplate);
        }

        if (actionTemplate != null)
        {
            actionData = Instantiate(actionTemplate);
        }
        
    }

    private void Start()
    {
        //TODO 读取数据
        //LoadData();
        inventoryUI.RefreshUI();
        actionUI.RefreshUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&beginnerGuidanceIndex<2&&!GameManager.Instatic.inventoryBeginnerGuidanceAccomplish&&bagPanel.activeSelf)
        {
            NextBeginnerGuidanceImage();
        }
    }

    public void SaveData()
    {
        SaveManager.Instatic.Save(inventoryData, inventoryData.name);
        SaveManager.Instatic.Save(actionData, actionData.name);
    }

    public void LoadData()
    {
        SaveManager.Instatic.Load(inventoryData, inventoryData.name);
        SaveManager.Instatic.Load(actionData, actionData.name);
    }
    
    
    #region 检查拖拽物品是否在每一个 Slot 范围中

    /// <summary>
    /// 检查拖拽物品是否在背包UI范围内
    /// </summary>
    /// <param name="position">鼠标拖拽的位置</param>
    /// <returns></returns>
    public bool CheckInInventoryUI(Vector2 position)
    {
        for (int i = 0; i < inventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                print(1);
                return true;
            }
        }

        print(2);
        return false;
    }

    /// <summary>
    /// 检查拖拽物品是否在快捷栏UI范围内
    /// </summary>
    /// <param name="position">鼠标拖拽的位置</param>
    /// <returns></returns>
    public bool CheckInActionyUI(Vector2 position)
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    public void OpenBag()
    {
        GameManager.Instatic.openMenu = true;
        bagPanel.SetActive(true);
        if (!GameManager.Instatic.inventoryBeginnerGuidanceAccomplish)
        {
            OpenBeginnerGuidance(0);
        }
        skeletonGraphic.gameObject.SetActive(false);
    }

    public void OpenBagAnimation()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "open", false);
        skeletonGraphic.gameObject.SetActive(true);
        Invoke("OpenBag",0.568f);
    }

    public void CloseBag()
    {
        bagPanel.SetActive(false);
        skeletonGraphic.AnimationState.SetAnimation(0, "close", false);
        skeletonGraphic.gameObject.SetActive(true);
        Invoke("CloseBagAnimation",0.568f);
    }

    public void CloseBagAnimation()
    {
        skeletonGraphic.gameObject.SetActive(false);
        GameManager.Instatic.openMenu = false;
    }

    public void OpenOrCloseBag()
    {
        isOpen = !isOpen;
        if (isOpen&&!GameManager.Instatic.openMenu)
        { 
            GameManager.Instatic.ForbidControl(1);
            OpenBagAnimation();
        }
        else
        {
            if (GameManager.Instatic.inventoryBeginnerGuidanceAccomplish)
            {
                CloseBag();
                GameManager.Instatic.AllowControl(1);
            }
            else
            {
                isOpen = !isOpen;
            }
        }
    }

    private void OpenBeginnerGuidance(int index)
    {
        GameManager.Instatic.ForbidControl(-1);
        if (index!=0)
        {
            beginnerGuidanceImage[index - 1].SetActive(false);
        }
        
        beginnerGuidanceImage[index].SetActive(true);
    }
    
    private void NextBeginnerGuidanceImage()
    {
        beginnerGuidanceIndex++;
        if (beginnerGuidanceIndex < 2)
        {
            OpenBeginnerGuidance(beginnerGuidanceIndex);
        }
        else
        {
            beginnerGuidanceImage[beginnerGuidanceIndex-1].SetActive(false);
            GameManager.Instatic.inventoryBeginnerGuidanceAccomplish = true;
            GameManager.Instatic.AllowControl(-1);
        }
    }

    public void OpenItemEnlargeDetailUI(ItemData_SO item)
    {
        if (item.itemEnlargeDetai)
        {
            GameManager.Instatic.ForbidControl();
            itemEnlargeDetai.sprite = item.detailsImage;
            itemEnlargeDetai.SetNativeSize();
            itemEnlargeDetailUI.SetActive(true);
        }
    }
}