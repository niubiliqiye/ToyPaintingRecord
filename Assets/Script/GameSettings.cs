using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSettings : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Transform selected;
    public GameObject pauseUI;
    public GameObject setingUI;
    private LoadManager loadManager;

    private void Awake()
    {
        loadManager=FindObjectOfType<LoadManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //当鼠标光标移入该对象时触发
        selected.gameObject.SetActive(true);
        selected.position = new Vector3(selected.position.x, transform.position.y, selected.position.z);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //当鼠标光标移出该对象时触发
        selected.gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1; 
        loadManager.sceneIndex = 0;
        loadManager.LoadNextLevel();
    }
    
}
