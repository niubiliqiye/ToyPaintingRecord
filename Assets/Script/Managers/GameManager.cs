using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    //public bool isGround;
    
    /// <summary>
    /// 解锁成就数量
    /// </summary>
    [Header("解锁成就数量")]public int unlockAchievementNumber;
    
    /// <summary>
    /// 玩家可以移动
    /// </summary>
    [Header("玩家是否可以操纵")]public bool isCanBeMove = true;

    /// <summary>
    /// 玩家可以操纵
    /// </summary>
    [Header("玩家是否可以操纵")]public bool isCanBeControl = true;

    /// <summary>
    /// 场景一新手任务完成
    /// </summary>
    [Header("场景一新手任务完成")]public bool oneSceneBeginnerGuidanceAccomplish;
    
    /// <summary>
    /// 背包新手教程完成
    /// </summary>
    [Header("背包新手教程完成")]public bool inventoryBeginnerGuidanceAccomplish;

    /// <summary>
    /// 人物对话教程完成
    /// </summary>
    [Header("人物对话教程完成")] public bool npcDialogueBeginnerGuidanceAccomplish;

    /// <summary>
    /// 绘画教程完成
    /// </summary>
    [Header("绘画教程完成")] public bool drawBeginnerGuidanceAccomplish;

    /// <summary>
    /// 全局音量大小
    /// </summary>
    [HideInInspector]public float volume=1;
    
    /// <summary>
    /// 是否可以绘画
    /// </summary>
    [Header("是否可以绘画")]public bool isDraw;

    /// <summary>
    /// 物品提示对话
    /// </summary>
    [Header("物品提示对话")] public bool itemTipDialogue;

    /// <summary>
    /// 打开菜单
    /// </summary>
    [Header("打开菜单")] public bool openMenu;

    /// <summary>
    /// 第一次进入破旧小屋
    /// </summary>
    [Header("第一次进入破旧小屋")] public bool theFirstTimeEnteredTheShabbyHut = true;

    /// <summary>
    /// 解锁钱府
    /// </summary>
    [Header("解锁钱府")]public bool unlockTheMoneyHouse;

    /// <summary>
    /// 地图教程完成
    /// </summary>
    [Header("地图教程完成")] public bool mapTutorialCompleted;

    /// <summary>
    /// 镜头移动
    /// </summary>
    [Header("镜头移动")] public bool cameraMove = true;

    /// <summary>
    /// 与包子铺老板完成第一次对话
    /// </summary>
    [Header("与包子铺老板完成第一次对话")]public bool completeTheFirstConversationWithTheOwnerOfTheDumplingShop;

    /// <summary>
    /// 人物交互UI教程完成
    /// </summary>
    [Header("人物交互UI教程完成")] public bool characterInteractiveUITutorialCompleted;

    /// <summary>
    /// 第一次与钱府管家对话
    /// </summary>
    [Header("第一次与钱府管家对话")] public bool theFirstTimeWithTheMoneyHouseButlerDialogue = true;

    /// <summary>
    /// 第一次与钱老爷对话
    /// </summary>
    [Header("第一次与钱老爷对话")] public bool firstTimeToTalkToQianMaste = true;

    /// <summary>
    /// 第一次与钱夫人对话
    /// </summary>
    [Header("第一次与钱夫人对话")] public bool firstTimeToTalkToMrsQian = true;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        //转换场景时不被销毁
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 禁止玩家操作
    /// </summary>
    /// <param name="operatorSchema">操作模式    -1：移动   0：移动和UI界面    1：UI界面   默认操作：移动和UI界面</param>
    public void ForbidControl(int operatorSchema=0)
    {
        switch (operatorSchema)
        {
            case -1:
                isCanBeMove = false;
                break;
            case 0:
                isCanBeMove = false;
                isCanBeControl = false;
                break;
            case 1:
                isCanBeControl = false;
                break;
            default:
                isCanBeMove = false;
                isCanBeControl = false;
                break;

        }
    }

    /// <summary>
    /// 允许玩家操作
    /// </summary>
    /// <param name="operatorSchema">操作模式    -1：移动   0：移动和UI界面    1：UI界面   默认操作：移动和UI界面</param>
    public void AllowControl(int operatorSchema=0)
    {
        switch (operatorSchema)
        {
            case -1:
                isCanBeMove = true;
                break;
            case 0:
                isCanBeMove = true;
                isCanBeControl = true;
                break;
            case 1:
                isCanBeControl = true;
                break;
            default:
                isCanBeMove = true;
                isCanBeControl = true;
                break;
        }
    }
}