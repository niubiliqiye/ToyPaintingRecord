using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePiece
{
    [Header("语句序列号")]public string ID;
    
    [Header("对话人名")]public string name;
    
    [TextArea]
    [Header("对话文本")]public string text;
    
    //public QuestData_SO quest;
    
    public List<DialogueOption> options = new List<DialogueOption>();
}
