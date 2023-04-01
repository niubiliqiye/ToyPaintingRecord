using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private LoadManager loadManager;
    private float time;
    private bool isEnter;
    public int sceneIndex;
    public GameObject tip;
    private TextMeshProUGUI tipText;

    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
        tipText = tip.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isEnter)
        {
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Scene scene = SceneManager.GetActiveScene ();
        tipText.text = "即将离开" + scene.name;
        tip.SetActive(true);
        if (col.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (time > 3)
        {
            loadManager.sceneIndex = sceneIndex;
            loadManager.LoadNextLevel();
            isEnter = false;
            time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tip.SetActive(false);
        isEnter = false;
        time = 0;
    }
}