using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneMap : MonoBehaviour
{
    /// <summary>
    /// 教程图片
    /// </summary>
    [Header("教程图片")]public GameObject tutorialPicture;

    /// <summary>
    /// 大街UI
    /// </summary>
    [Header("大街UI")] public GameObject avenueUI;

    /// <summary>
    /// 地图触发器
    /// </summary>
    [Header("地图触发器")] public BoxCollider2D[] boxCollider2D;
    
    private void Start()
    {
        if (!GameManager.Instatic.mapTutorialCompleted)
        {
            for (int i = 0; i < boxCollider2D.Length; i++)
            {
                boxCollider2D[i].enabled = false;
            }
            GameManager.Instatic.cameraMove = false;
            tutorialPicture.SetActive(true);
            avenueUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&tutorialPicture.activeSelf&&!GameManager.Instatic.mapTutorialCompleted)
        {
            for (int i = 0; i < boxCollider2D.Length; i++)
            {
                boxCollider2D[i].enabled = true;
            }
            GameManager.Instatic.mapTutorialCompleted = true;
            tutorialPicture.SetActive(false);
            avenueUI.SetActive(false);
            GameManager.Instatic.cameraMove = true;
        }
    }
}
