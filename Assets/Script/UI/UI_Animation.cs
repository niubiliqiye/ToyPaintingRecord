using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class UI_Animation : MonoBehaviour
{
    public  SkeletonGraphic skeleton;
    private string openName = "open";
    private string idleName = "openidle";
    private string clickBirdName = "openclickbird";
    private string closeName = "close";
    private string closeIdleName = "closeidle";
    public bool isOpen;
    private Install install;
    private Archive archive;
    private LoadManager loadManager;


    private void Awake()
    {
        skeleton = GetComponent<SkeletonGraphic>();
        install = FindObjectOfType<Install>();
        archive = FindObjectOfType<Archive>();
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&GameManager.Instatic.isCanBeControl)
        {
            isOpen = !isOpen;
            UI_Animation_Switch();
        }
    }

    private void UI_Animation_Open()
    {
        skeleton.AnimationState.AddAnimation(0, gameObject.tag + "-" + openName, false,0f);
        Invoke("UI_Animation_OpenIdle", 0.5f);
    }

    private void UI_Animation_OpenIdle()
    {
        skeleton.AnimationState.SetAnimation(0, gameObject.tag + "-" + idleName,true);

    }

    public void UI_Animation_OpenClickBird()
    {
        if (isOpen)
        {
            skeleton.AnimationState.SetAnimation(0, gameObject.tag + "-" + clickBirdName, false);
            Invoke("UI_Animation_OpenIdle", 2f);
        }
    }

    private void UI_Animation_Close()
    {
        skeleton.AnimationState.AddAnimation(0, gameObject.tag + "-" + closeName, false,0f);
        Invoke("UI_Anmiation_CloseIdle", 0.468f);
    }

    private void UI_Anmiation_CloseIdle()
    {
        skeleton.AnimationState.SetAnimation(0, gameObject.tag + "-" + closeIdleName, true);
    }

    private void UI_Animation_Switch()
    {
        if (isOpen)
        {
            GameManager.Instatic.ForbidControl(-1);
            UI_Animation_Open();
        }
        else
        {
            UI_Animation_Close();
            GameManager.Instatic.AllowControl(-1);
        }
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    public void OpenOrCloseBag()
    {
        if (isOpen)
        {
            InventoryManager.Instatic.OpenOrCloseBag();
        }
    }

    /// <summary>
    /// 打开设置
    /// </summary>
    public void OpenInstall()
    {
        if (isOpen&&!install.GetComponent<Install>().install.activeSelf&&!GameManager.Instatic.openMenu)
        {
            GameManager.Instatic.ForbidControl(1);
            install.GetComponent<Install>().Install_Open();
            install.GetComponent<Install>().install.SetActive(true);
        }
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        if (isOpen)
        {
            GameManager.Instatic.AllowControl();
            //TODO:保存游戏
            loadManager.LoadNextLevel();
        }
    }

    /// <summary>
    /// 打开存档
    /// </summary>
    public void OpenArchive()
    {
        if (isOpen&&!archive.skeletonGraphic.gameObject.activeSelf&&!GameManager.Instatic.openMenu)
        {
            GameManager.Instatic.openMenu = true;
            GameManager.Instatic.ForbidControl(1);
            archive.Open();
        }
        else
        {
            GameManager.Instatic.openMenu = false;
            archive.Close();
            GameManager.Instatic.AllowControl(1);
        }
    }
}
