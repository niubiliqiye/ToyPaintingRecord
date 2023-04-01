using System;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

namespace Script.Inventory.Item.MonoBehavior
{
    public class ItemPickUp : MonoBehaviour
    {
        [Header("物品信息")] public ItemData_SO itemData;
        private bool isbutton;
        private bool isEnier;
        public GameObject dialogueUI;
        public DialogueData_SO dialogueDataSo;

        // Start is called before the first frame update 
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                isEnier = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isEnier = false;
        }

        private void OnMouseUpAsButton()
        {
            if (isEnier)
            {
                if (itemData.details)
                {
                    isbutton = true;
                    InventoryManager.Instatic.tooltip02.PickUpTooltip(itemData);
                    InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
                }
                else
                {
                    isbutton = true;
                }
            }

        }

        private void Update()
        {
            if (isEnier)
            {
                if (isbutton)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        isbutton = false;
                        //isEnier = false;
                        if (InventoryManager.Instatic.tooltip02.tipPanel.activeSelf)
                        {
                            InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
                        }
                        
                    }
                        if (itemData.isPickUp)
                        {
                            //TODO:将物品添加到背包
                            InventoryManager.Instatic.inventoryData.AddItem(itemData, itemData.itemAmount);
                            InventoryManager.Instatic.inventoryUI.RefreshUI();
                            Destroy(gameObject);
                            //打开UI面板
                            dialogueUI.SetActive(true);
                            //传输对话内容信息
                            DialogueUI.Instatic.UpdateDialogueData(dialogueDataSo);
                            DialogueUI.Instatic.UpdateMainDialogue(dialogueDataSo.dialoguePieces[0]);
                        }
                    
                }
            }
        }
    }
}