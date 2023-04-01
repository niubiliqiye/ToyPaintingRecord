using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    private Draw draw;
    private PaintingSystem paintingSystem;
    /// <summary>
    /// 是否为重置键
    /// </summary>
    [Header("是否为重置键")]public bool isReset;

    private void Awake()
    {
        draw = FindObjectOfType<Draw>();
        paintingSystem = FindObjectOfType<PaintingSystem>();
    }

    private void OnMouseDown()
    {
        if (isReset)
        {
            draw.ResetCanvas();
        }
        else
        {
            paintingSystem.Detection();
        }
    }
}
