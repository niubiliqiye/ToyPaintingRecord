using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadManager : MonoBehaviour
{
    [Header("提示数据")]public ToolTipData_SO toolTipDataSo;
    
    [Header("提示")]public TextMeshProUGUI toolTip;

    [Header("加载界面")] public GameObject loadScreen;
    
    public SkeletonGraphic skeletonGraphic;
    
    public GameObject panel;


    //[Header("进度条")] public Slider slider;

    public TextMeshProUGUI text;

    [Header("需要加载的场景编号")] public int sceneIndex;
    private AsyncOperation operation;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLeve());
    }

    IEnumerator LoadLeve()
    {
        loadScreen.SetActive(true);
        panel.SetActive(true);
        skeletonGraphic.gameObject.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        //TODO 
        toolTip.text = toolTipDataSo.tooltip[Random.Range(0, toolTipDataSo.tooltip.Length)];
        while (!operation.isDone)
        {
            //slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                //slider.value = 1;
                text.text = "按任意键继续";
                bool isNextScene = true;
                if (Input.anyKeyDown&&isNextScene)
                {
                    isNextScene = false;
                    loadScreen.SetActive(false);
                    skeletonGraphic.AnimationState.SetAnimation(0, "close", false);
                    Invoke("NextScene",3f);
                }
            }
            
            yield return null;
        }
    }


    private void NextScene()
    {
        operation.allowSceneActivation = true;
    }
}
