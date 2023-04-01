using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Inventory.Item.ScriptableObject;
using TMPro;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    /// <summary>
    /// 用于保存商店数据
    /// </summary>
    public InventoryData_SO shopTemplate;
    
    [Header("Shop Data")] [Tooltip("商店列表数据")]
    public InventoryData_SO shopData;
    
    [Header("ContainerS")] [Tooltip("商店UI")]
    public ContainerUI shopUI;
    
    [Header("物品信息提示框")] public ItemTooltip tooltip;
    
    [Header("商店面板")] public GameObject shopPanel;

    /// <summary>
    /// 元宝价格
    /// </summary>
    public TextMeshProUGUI ingotPrice;
    
    /// <summary>
    /// 铜钱价格
    /// </summary>
    public TextMeshProUGUI copperCoinPrice;

    /// <summary>
    /// 当前选中的商品
    /// </summary>
    public ItemData_SO theCurrentlySelectedItem;

    /// <summary>
    /// 当前选中的商品的格子
    /// </summary>
    public SlotHolder theCurrentlySelectedItemCell;

    /// <summary>
    /// 正在与玩家交互的NPC
    /// </summary>
    public TestNpc testNpc;

    /// <summary>
    /// 元宝数量
    /// </summary>
    public TextMeshProUGUI ingotNumber;
    
    /// <summary>
    /// 铜钱数量
    /// </summary>
    public TextMeshProUGUI copperCoinNumber;
    

    protected override void  Awake()
    {
        base.Awake();
        if (shopTemplate != null)
        {
            shopData = Instantiate(shopTemplate);
        }
    }
    
    private void Start()
    {
        //TODO 读取数据
        //LoadData();
        //shopUI.RefreshUI();
    }
    
    public void SaveData()
    {
        SaveManager.Instatic.Save(shopData, shopData.name);
    }

    public void LoadData()
    {
        SaveManager.Instatic.Load(shopData, shopData.name);
    }
    
    /// <summary>
    /// 更新玩家持有的钱数
    /// </summary>
    public void Refresh()
    {
        ingotNumber.text = Money.Instatic.ingotNumber.ToString();
        copperCoinNumber.text = Money.Instatic.copperCoinNumber.ToString();
    }
}
