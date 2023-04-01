using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class SelectMap : MonoBehaviour
{
    /// <summary>
    /// 需要跳转的场景
    /// </summary>
    [Header("需要跳转的场景")]public int sceneIndex;
    
    /// <summary>
    /// 提示框
    /// </summary>
    [Header("提示框")]public GameObject tip;

    /// <summary>
    /// 地图名称
    /// </summary>
    [Header("地图名称")]public GameObject mapName;
    private TextMeshProUGUI tipText;
    private LoadManager loadManager;
    public bool needClue;

    private void Awake()
    {
        tipText = tip.GetComponentInChildren<TextMeshProUGUI>();
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void OnMouseEnter()
    {
        // tipText.text = "单击进入" + gameObject.name;
        // tip.SetActive(true);
        if (!needClue||GameManager.Instatic.unlockTheMoneyHouse)
        {
            mapName.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        // tip.SetActive(false);
        if (!needClue||GameManager.Instatic.unlockTheMoneyHouse)
        {
            mapName.SetActive(false);
        }
    }
    
    private void OnMouseDown()
    {
        if (!needClue||GameManager.Instatic.unlockTheMoneyHouse)
        {
            loadManager.sceneIndex = sceneIndex;
            loadManager.LoadNextLevel();
        }
    }
}
