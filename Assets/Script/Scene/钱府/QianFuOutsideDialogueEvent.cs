using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QianFuOutsideDialogueEvent : MonoBehaviour
{
    private LoadManager loadManager;

    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }

    public void EnterQianFu()
    {
        GameManager.Instatic.AllowControl();
        GameManager.Instatic.theFirstTimeWithTheMoneyHouseButlerDialogue = false;
        loadManager.sceneIndex = 11;
        loadManager.LoadNextLevel();
    }
}
