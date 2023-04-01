using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    [Header("对话文本")]public string text;
    
    [Header("需要跳转的行数")]public string targetID;
    
    [Header("是否触发事件")]public bool takeQuest;
    
    [Header("调用函数所在的类挂载的物体")]public string gameobjectName;
    
    [Header("调用函数的函数名")] public string functionName;
}
