using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Script;
using Spine.Unity;
using TMPro;

public class DialogueUI : Singleton<DialogueUI>
{
    [Header("Basic Element")] public Image icon;

    [Header("姓名框")] public TextMeshProUGUI nameBox;
    public GameObject NameBox;

    public Text mainText;

    public Button nextButton;

    public GameObject dialoguePanel;
    
    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    [Header("Data")] public DialogueData_SO currentData;

    private int currentIndex = 0;

    private Player player;
    private DoorOfTheDepartment doorOfTheDepartment;
    private Prison prison;
    private OutsideTheShukaku outsideTheShukaku;
    private ShuKaKu shuKaKu;
    private QianMaster qianMaster;

    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);
        player = FindObjectOfType<Player>();
        doorOfTheDepartment = FindObjectOfType<DoorOfTheDepartment>();
        prison = FindObjectOfType<Prison>();
        outsideTheShukaku = FindObjectOfType<OutsideTheShukaku>();
        shuKaKu = FindObjectOfType<ShuKaKu>();
        qianMaster = FindObjectOfType<QianMaster>();
    }

    void ContinueDialogue()
    {
        if (currentIndex < currentData.dialoguePieces.Count)
        {
            UpdateMainDialogue((currentData.dialoguePieces[currentIndex]));
        }
        else
        {
            // NameBox.gameObject.SetActive(false);
            //dialoguePanel.SetActive(false);
            gameObject.SetActive(false);
            GameManager.Instatic.AllowControl();
            if (player.skeletonAnimationNPC != null)
            {
                player.skeletonAnimationNPC.AnimationName = "idle";
                switch (player.skeletonAnimationNPC.gameObject.GetComponent<Npc>().npcName)
                {
                    case "户部公公":
                        InventoryManager.Instatic.inventoryData.DeleteItem(player.skeletonAnimationNPC.gameObject.GetComponent<Eunuch>().DeleteIten);
                        if (!InventoryManager.Instatic.inventoryData.FindItem<bool>(player.skeletonAnimationNPC.gameObject
                                .GetComponent<Eunuch>().DeleteIten)&&!doorOfTheDepartment.theFirstSegmentIsOver)
                        {
                            doorOfTheDepartment.isDeleteItem = true;
                            // gameObject.SetActive(true);
                            // UpdateDialogueData(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[0]);
                            // UpdateMainDialogue(FindObjectOfType<DoorOfTheDepartment>().dialogueDataSos[0].dialoguePieces[0]);
                        }

                        if (!doorOfTheDepartment.isDeleteItem&&!doorOfTheDepartment.theSecondSegmentIsOver)
                        {
                            doorOfTheDepartment.isTheSecondDialogue = true;
                        }
                        break;
                    case "常衡":
                        prison.theSecondSegmentIsOver = true;
                        player.skeletonAnimationNPC = null;
                        break;
                    default:
                        if (player.skeletonAnimationNPC.gameObject.GetComponent<Npc>().npcUI!=null)
                        {
                            player.skeletonAnimationNPC.gameObject.GetComponent<Npc>().npcUI.isUIOpen = false;
                        }
                        break;
                }
            }

            EndEvent();
            
            // if (player.skeletonAnimationNPC.gameObject.GetComponent<Npc>().npcName != "太监")
            // {
            //     player.skeletonAnimationNPC.gameObject.GetComponent<TestNpc>().npcUI.isUIOpen = false;
            // }
            // else
            // {
            //     InventoryManager.Instatic.inventoryData.DeleteItem(player.skeletonAnimationNPC.gameObject.GetComponent<Eunuch>().DeleteIten);
            //     if (!InventoryManager.Instatic.inventoryData.FindItem(player.skeletonAnimationNPC.gameObject
            //             .GetComponent<Eunuch>().DeleteIten))
            //     {
            //         doorOfTheDepartment.isDeleteItem = true;
            //     }
            // }
        }
    }

    /// <summary>
    /// 更新对话数据
    /// </summary>
    /// <param name="data">对话数据</param>
    public void UpdateDialogueData(DialogueData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }

    /// <summary>
    /// 更新对话主界面
    /// </summary>
    /// <param name="piece">当前对话</param>
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);
        currentIndex++;
        
        if (piece.name != null)
        {
            nameBox.enabled = true;
            nameBox.text = piece.name;
        }
        else
        {
            nameBox.enabled = false;
        }

        mainText.text = "";
        //mainText.text = piece.text;
        //利用插件实现文本逐字显示
        mainText.DOText(piece.text, 1f);

        if (piece.options.Count == 0 && currentData.dialoguePieces.Count > 0)
        {
            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
            nextButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            //nextButton.gameObject.SetActive(false);
            //将Button点按功能取消
            nextButton.interactable = false;
            nextButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        //创建option
        CreateOptions(piece);
    }

    /// <summary>
    /// 创建回答选项
    /// </summary>
    /// <param name="piece"></param>
    void CreateOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount >= 0)
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }

            for (int i = 0; i < piece.options.Count; i++)
            {
                var option = Instantiate(optionPrefab, optionPanel);
                option.UpdteOption(piece, piece.options[i]);
            }
        }
    }

    /// <summary>
    /// 对话结束事件
    /// </summary>
    private void EndEvent()
    {
        if (prison != null)
        {
            if (currentData == prison.dialogueDataSo[2])
            {
                prison.theThirdlySegmentIsOver = true;
            }
        }
            
        if (outsideTheShukaku != null)
        {
            if (currentData == outsideTheShukaku.dialogueDataSo[1])
            {
                outsideTheShukaku.permitPainting = true;
            }

            if (currentData == outsideTheShukaku.dialogueDataSo[2])
            {
                outsideTheShukaku.itemTipDialogueAccomplish = true;
            }
        }

        if (shuKaKu!=null)
        {
            if (currentData==shuKaKu.dialogueDataSo[0])
            {
                shuKaKu.openHuaRoaD = true;
            }

            if (currentData==shuKaKu.dialogueDataSo[1])
            {
                shuKaKu.startCountDown = true;
            }

            if (currentData==shuKaKu.dialogueDataSo[2])
            {
                shuKaKu.outcomeOne=true;
            }

            if (currentData==shuKaKu.dialogueDataSo[3])
            {
                shuKaKu.theFourthDialogEnds = true;
            }
        }

        if (qianMaster!=null)
        {
            if (currentData==qianMaster.dialogFile)
            {
                qianMaster.theFirstParagraphIsFinished = true;
            }

            if (currentData==qianMaster.theSecondDialogue)
            {
                Money.Instatic.ingotNumber = 20;
                GameManager.Instatic.theNumberOfTimesThePlayerDrawsForMasterQian += 1;
                GameManager.Instatic.firstTimeToTalkToQianMaste = false;
            }
        }
    }
}