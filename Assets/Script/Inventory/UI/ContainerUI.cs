using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public SlotHolder[] slotHolders;
    
    /// <summary>
    /// 刷新UI
    /// </summary>
    public void RefreshUI()
    {
        for (int i = 0; i < slotHolders.Length; i++)
        {
            slotHolders[i].itemUI.Index = i;
            slotHolders[i].UpdateItem();
        }

        InventoryManager.Instatic.copperCoinNumber.text = Money.Instatic.copperCoinNumber.ToString();
        InventoryManager.Instatic.ingotNumber.text = Money.Instatic.ingotNumber.ToString();
    }
}
