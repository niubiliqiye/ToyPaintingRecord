using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [Header("当前选项的文本组件")]public TextMeshProUGUI optionText;
    
    [Header("选择框")]public Image choiceBox;
    
    private Button thisButton;
    
    /// <summary>
    /// 当前选项匹配的对话
    /// </summary>
    private DialoguePiece currentPiece;

    private string nextPieceID;

    /// <summary>
    /// 是否触发事件
    /// </summary>
    private bool isTriggerEvent;

    /// <summary>
    /// 触发事件的名字
    /// </summary>
    private string triggerEventName;

    /// <summary>
    /// 事件函数所在物体的名字 
    /// </summary>
    private string triggerEventGameObjectName;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }

    public void UpdteOption(DialoguePiece piece, DialogueOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        isTriggerEvent = option.takeQuest;
        triggerEventGameObjectName = option.gameobjectName;
        triggerEventName = option.functionName;
    }

    public void OnOptionClicked()
    {
        if (nextPieceID == "")
        {
            if (isTriggerEvent)
            {
                GetFunction(triggerEventGameObjectName,triggerEventName);
            }
            DialogueUI.Instatic.gameObject.SetActive(false);
            GameManager.Instatic.AllowControl();
            return;
        }
        else
        {
            if (isTriggerEvent)
            {
                GetFunction(triggerEventGameObjectName,triggerEventName);
            }
            //Debug.Log(DialogueUI.Instatic.currentData.dialogueIndex[nextPieceID]);
            DialogueUI.Instatic.UpdateMainDialogue(DialogueUI.Instatic.currentData.dialogueIndex[nextPieceID]);
        }
        
    }

    private void OnMouseEnter()
    {
        choiceBox.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        choiceBox.gameObject.SetActive(false);
    }

    /// <summary>
    /// 获取相应的函数并调用
    /// </summary>
    /// <param name="gameobjectName">事件函数所在物体的名字 </param>
    /// <param name="functionName">触发事件的名字</param>
    private void GetFunction(string gameobjectName,string functionName)
    {
        GameObject.Find(gameobjectName).SendMessage(functionName);
    }
}
