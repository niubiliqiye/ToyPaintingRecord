using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    private LoadManager loadManager;

    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            loadManager.LoadNextLevel();
        }
    }
}
