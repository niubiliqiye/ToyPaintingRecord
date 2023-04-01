using UnityEngine;

//生成物品属性的Ass资源
namespace Script.Inventory.Item.ScriptableObject
{
    /// <summary>
    /// 物品种类
    /// Useable：消耗物
    /// Prop：道具类
    /// Commodity:商品
    /// </summary>
    public enum ItemType{Useable,Prop,Commodity}
    [CreateAssetMenu(fileName = "new Item",menuName = "Inventory/Item Date")]
    public class ItemData_SO : UnityEngine.ScriptableObject
    {
        [Header("物品类型")][Tooltip("Useable：消耗物   Prop：道具类    Commodity：商品类")]public ItemType itemType;

        [Header("物品名字")]public string itemName;

        [Header("物品图标")]public Sprite itemIcon;

        [Header("物品数量")] public int itemAmount;
        [Header("物品能否捡起")] public bool isPickUp;

        [Header("物品是否能堆叠")] [Tooltip("true：可堆叠   false：不可堆叠")]
        public bool stackable;

        /// <summary>
        /// 是否有详情界面
        /// </summary>
        [Header("是否有详情界面")] public bool details;

        /// <summary>
        /// 是否有放大详情
        /// </summary>
        [Header("是否有放大详情")]public bool itemEnlargeDetai;

        /// <summary>
        /// 详情图片
        /// </summary>
        [Header("详情图片")]public Sprite detailsImage;

        //[Header("详情界面是显示完")] public bool isdetails;
        
        [Header("物品介绍")] [TextArea]public string description = "";
        //[Header("Useable Item")] public UseableItemData_SO itemData;

        /// <summary>
        /// 元宝价格
        /// </summary>
        [Header("元宝价格")] public int ingotPrice;
        
        /// <summary>
        /// 铜钱价格
        /// </summary>
        [Header("铜钱价格")] public int copperCoinPrice;

        /// <summary>
        /// 物品增加的好感值
        /// </summary>
        [Header("好感值")] public int favorabilityValue;
    }
}