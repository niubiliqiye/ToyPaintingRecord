using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script.Inventory.Item.ScriptableObject;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prison : MonoBehaviour
{
    /// <summary>
    /// 玩家是否与常衡对话完毕
    /// </summary>
    public bool isDialog;

    /// <summary>
    /// 黑色提示框
    /// </summary>
    public GameObject blackTips;

    /// <summary>
    /// 提示
    /// </summary>
    public GameObject tip;

    /// <summary>
    /// 常衡
    /// </summary>
    [Header("常衡")] public GameObject changHeng;

    /// <summary>
    /// 监狱守卫
    /// </summary>
    [Header("监狱守卫")]public GameObject shouWei;
    
    [Header("时间切换")] public SpriteRenderer timeSprite;

    /// <summary>
    /// 黑夜贴图
    /// </summary>
    [Header("黑夜图片")] public Sprite nightSprite;

    /// <summary>
    /// 是否解锁成功
    /// </summary>
    public bool isUnlockingSucceeded;

    /// <summary>
    /// 钥匙
    /// </summary>
    [Header("钥匙")]public ItemData_SO key;

    /// <summary>
    /// 大牢门是否打开
    /// </summary>
    [Header("大牢门是否打开")]public bool isOpenTheDoor;

    /// <summary>
    /// 大牢牢门
    /// </summary>
    [Header("大牢牢门")] public SpriteRenderer[] doors = new SpriteRenderer[2];

    [Header("绘画面板")] public GameObject drawPanel;

    private TextMeshProUGUI blackTipsText;
    private TextMeshProUGUI TipText;
    private GameObject player;
    public GameObject nextScene;

    public GameObject dialogueUI;
    public DialogueData_SO[] dialogueDataSo;
    
    public bool theFirstSegmentIsOver;
    public bool theSecondSegmentIsOver;
    public bool theThirdlySegmentIsOver;
    public bool theFourthlySegmentIsOver;
    /// <summary>
    /// 新手引导图片
    /// </summary>
    [Header("新手引导图片")]public GameObject[] beginnerGuidanceImage;
    
    private int beginnerGuidanceIndex=0;


    private void Awake()
    {
        blackTipsText = blackTips.GetComponentInChildren<TextMeshProUGUI>();
        TipText = tip.GetComponentInChildren<TextMeshProUGUI>();
        player=GameObject.Find("Player");
    }

    private void Start()
    {
        OpenDialogue(0);
    }

    private void Update()
    {
        if (!dialogueUI.activeSelf && !theFirstSegmentIsOver)
        {
            theFirstSegmentIsOver = true;
            blackTips.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 1f);
            blackTips.GetComponent<TimedShutdown>().enabled = true;
            blackTips.GetComponent<TimedShutdown>().lengthOfTime = 1.5f;
        }
        if (theSecondSegmentIsOver)
        {
            theSecondSegmentIsOver = false;
            Invoke("OpenBlackTips",0.5f);
        }

        if (isUnlockingSucceeded)
        {
            isUnlockingSucceeded = false;
            InventoryManager.Instatic.tooltip02.PickUpTooltip(key);
            InventoryManager.Instatic.tooltip02.tipPanel.SetActive(true);
            InventoryManager.Instatic.inventoryData.AddItem(key, 1);
            InventoryManager.Instatic.inventoryUI.RefreshUI();
            Invoke("CloseTooltip",1f);
        }

        if (isOpenTheDoor)
        {
            print(2);
            isOpenTheDoor = false;
            InventoryManager.Instatic.inventoryData.DeleteItem(key);
            InventoryManager.Instatic.inventoryUI.RefreshUI();
            blackTipsText.text = "遇河使用钥匙打开了门锁";
            blackTips.SetActive(true);
            Invoke("CloseBlackTips02",2f);
            shouWei.GetComponent<MeshRenderer>().sortingLayerName = "CentreGround";
            doors[0].sortingLayerName = "CentreGround";
            doors[1].sortingLayerName = "CentreGround";
            nextScene.SetActive(true);
        }

        // if (theFourthlySegmentIsOver)
        // {
        //     theFourthlySegmentIsOver = false;
        //     //OpenDialogue(0);
        //     //drawPanel.SetActive(true);
        //
        // }

        if (theThirdlySegmentIsOver)
        {
            theThirdlySegmentIsOver = false;
            drawPanel.SetActive(true);
            if (!GameManager.Instatic.oneSceneBeginnerGuidanceAccomplish)
            {
                BeginnerGuidance(beginnerGuidanceIndex);
            }
        }

        if (Input.GetMouseButtonDown(0)&&beginnerGuidanceIndex<2&&!GameManager.Instatic.oneSceneBeginnerGuidanceAccomplish&&beginnerGuidanceImage[beginnerGuidanceIndex].activeSelf)
        {
            NextBeginnerGuidanceImage();
        }
    }

    private void OpenBlackTips()
    {
        blackTips.SetActive(true);
        blackTips.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
        blackTipsText.text = "过了一段时间";
        blackTips.GetComponent<TimedShutdown>().enabled = false;
        Invoke("CloseBlackTips01", 3.5f);
    }

    private void CloseBlackTips01()
    {

        changHeng.SetActive(false);
        shouWei.GetComponent<MeshRenderer>().sortingLayerName = "ForeGround";
        shouWei.SetActive(true);
        blackTips.SetActive(false);
        timeSprite.sprite = nightSprite;
        //打开UI面板
        OpenDialogue(1);
        //drawPanel.SetActive(true);
    }

    private void CloseBlackTips02()
    {
        print(3);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.5f,
            player.transform.position.z);
        blackTips.SetActive(false);
        OpenDialogue(3);
    }

    private void CloseTooltip()
    {
        InventoryManager.Instatic.tooltip02.tipPanel.SetActive(false);
    }
    public void OpenDialogue(int index)
    {
        GameManager.Instatic.ForbidControl();
        //打开UI面板
        dialogueUI.SetActive(true);
        //传输对话内容信息
        DialogueUI.Instatic.UpdateDialogueData(dialogueDataSo[index]);
        DialogueUI.Instatic.UpdateMainDialogue(dialogueDataSo[index].dialoguePieces[0]);
    }
    
    private void BeginnerGuidance(int index)
    {
        GameManager.Instatic.ForbidControl();
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
            BeginnerGuidance(beginnerGuidanceIndex);
        }
        else
        {
            beginnerGuidanceImage[beginnerGuidanceIndex-1].SetActive(false);
            GameManager.Instatic.drawBeginnerGuidanceAccomplish = true;
            GameManager.Instatic.isDraw = true;
        }
    }
}
