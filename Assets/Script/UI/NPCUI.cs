using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class NPCUI : MonoBehaviour
{
    /// <summary>
    /// 交互总界面
    /// </summary>
    [Header("界面")] public GameObject all;

    /// <summary>
    /// 好感度界面
    /// </summary>
    [Header("好感")] public GameObject favorability;

    /// <summary>
    /// 礼物界面
    /// </summary>
    [Header("礼物")] public GameObject gift;

    /// <summary>
    /// 交互动画脚本
    /// </summary>
    [Header("人物交互动画")] public SkeletonAnimation skeletonAnimation;

    [Header("角色动画")] public SkeletonAnimation skeletonAnimationNpc;

    [Header("对话控制脚本")] public DialogueController dialogueController;

    /// <summary>
    /// 鼠标是否离开界面
    /// </summary>
    private bool isMouseExit;

    public string UIInterface;

    /// <summary>
    /// UI是否打开
    /// </summary>
    public bool isUIOpen;

    /// <summary>
    /// 设置动画片段并播放
    /// </summary>
    public void OpenAnimation()
    {
        isUIOpen = true;
        skeletonAnimation.AnimationName = "all-open";
        skeletonAnimation.gameObject.SetActive(true);
        Invoke("OpenInterface",0.434f);
    }

    /// <summary>
    /// 打开交互总界面,关闭动画界面
    /// </summary>
    private void OpenInterface()
    {
        skeletonAnimation.gameObject.SetActive(false);
        all.SetActive(true);
    }

    /// <summary>
    /// 关闭交互总界面并打开设置动画
    /// </summary>
    private void CloseInterface()
    {
        all.SetActive(false);
        skeletonAnimation.AnimationName = "all-close";
        skeletonAnimation.gameObject.SetActive(true);
        Invoke("CloseAnimation",0.434f);
    }

    /// <summary>
    /// 关闭动画
    /// </summary>
    private void CloseAnimation()
    {
        skeletonAnimation.gameObject.SetActive(false);
        isUIOpen = false;
    }

    /// <summary>
    /// 选择功能后关闭交互总界面
    /// </summary>
    public void SwitchCloseInterface()
    {
        all.SetActive(false);
        skeletonAnimation.AnimationName = "all-switchclose";
        skeletonAnimation.gameObject.SetActive(true);
        if (UIInterface == "dialog")
        {
            Invoke("OpenBranch",0.51f);
        }
        else
        {
            Invoke("OpenBranchAnimation",0.51f);
        }
    }

    /// <summary>
    /// 从其他界面返回交互总界面
    /// </summary>
    private void SwitchOpenInterface()
    {
        skeletonAnimation.gameObject.SetActive(false);
        skeletonAnimation.AnimationName = "all-switchopen";
        skeletonAnimation.gameObject.SetActive(true);
        Invoke("OpenInterface",0.434f);
    }

    /// <summary>
    /// 打开交互其他界面动画
    /// </summary>
    private void OpenBranchAnimation()
    {
        skeletonAnimation.gameObject.SetActive(false);
        skeletonAnimation.AnimationName = "branch-open";
        skeletonAnimation.gameObject.SetActive(true);
        Invoke("OpenBranch",0.434f);
    }

    /// <summary>
    /// 关闭交互其他界面动画
    /// </summary>
    private void CloseBranchAnimation()
    {
        skeletonAnimation.AnimationName = "branch-close";
        skeletonAnimation.gameObject.SetActive(true);
        Invoke("SwitchOpenInterface",0.434f);
    }

    private void OpenBranch()
    {
        switch (UIInterface)
        {
            case "dialog":
                skeletonAnimation.gameObject.SetActive(false);
                dialogueController.OpenDialogue();
                skeletonAnimationNpc.AnimationName = "huida";
                break;
            case "favorability":
                favorability.SetActive(true);
                break;
            case "gift":
                gift.SetActive(true);
                break;
        }
        skeletonAnimation.gameObject.SetActive(false);
    }

    private void CloseBranch()
    {
        switch (UIInterface)
        {
            case "dialog":
                break;
            case "favorability":
                favorability.SetActive(false);
                CloseBranchAnimation();
                break;
            case "gift":
                gift.SetActive(false);
                CloseBranchAnimation();
                break;
        }
    }

    private void OnMouseEnter()
    {
        isMouseExit = false;
    }

    private void OnMouseExit()
    {
        isMouseExit = true;
    }

    private void Close()
    {
        //print(isMouseExit);
        if (UIInterface != "dialog"&&Input.GetMouseButtonDown(0)&&isMouseExit&&!all.activeSelf)
        {
            if ((UIInterface == "favorability" && favorability.activeSelf)||(UIInterface == "gift" && gift.activeSelf))
            {
                CloseBranch();
            }
        }
        else if (Input.GetMouseButtonDown(0) && isMouseExit && all.activeSelf)
        {
            CloseInterface();
        }
    }
    
    private void Update()
    {
        Close();
    }
}
