using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class MenBlack : UnlockAchievement
{
    private bool isPlayerEnter;
    private void Awake()
    {
        index = 1;
        showAchievement = FindObjectOfType<ShowAchievement>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)&&!isUnlockAchievement&&isPlayerEnter)
        {
            showAchievement.Show(index);
            isUnlockAchievement = true;
            GameManager.Instatic.unlockAchievementNumber = index;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerEnter = false;
    }
}
