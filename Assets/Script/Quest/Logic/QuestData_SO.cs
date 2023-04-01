using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest Data")]
public class QuestData_SO : ScriptableObject
{
    [System.Serializable]
    public class QuestRequire
    {
        
        /// <summary>
        /// 任务需要的东西
        /// </summary>
        public string name;

        /// <summary>
        /// 物品数量
        /// </summary>
        public int requireAmount;

        /// <summary>
        /// 当前物品数量
        /// </summary>
        public int currentAmount;
    }
    
    [Header("任务名字")] public string questName;

    [TextArea] [Header("任务描述")] public string description;

    /// <summary>
    /// 任务开始
    /// </summary>
    [Header("任务状态")] public bool isStarted;

    /// <summary>
    /// 任务完成
    /// </summary>
    public bool isComplete;

    /// <summary>
    /// 任务完成同时奖励也领完
    /// </summary>
    public bool isFinished;

    public List<QuestRequire> questRequires = new List<QuestRequire>();
}