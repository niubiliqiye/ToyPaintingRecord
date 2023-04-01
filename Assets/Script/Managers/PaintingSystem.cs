using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class PaintingSystem : MonoBehaviour
{
    private Prison prison;
    private OutsideTheShukaku outsideTheShukaku;
    public GameObject tip;
    private TextMeshProUGUI tipText;
    public SpriteRenderer spriteRenderer;
    public GameObject draw;
    
    /// <summary>
    /// 目标纹理
    /// </summary>
    [Header("目标纹理")]public Texture2D targettexture2D;
    private Texture2D texture2D;
    private Color[] colors;
    private Color[] colors02;
    
    /// <summary>
    /// 相同像素的数量
    /// </summary>
    private double samePixelQuantity=0;

    /// <summary>
    /// 相似度
    /// </summary>
    private double similarity;

    /// <summary>
    /// 相似度指标
    /// </summary>
    [Header("相似度指标")]public double similarityIndex=0.8;

    private void Awake()
    {
        texture2D = spriteRenderer.sprite.texture;
        prison = FindObjectOfType<Prison>();
        outsideTheShukaku = FindObjectOfType<OutsideTheShukaku>();
        tipText = tip.GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// 对比两张图片是否相同
    /// </summary>
    private void ContrastTexture2D()
    {
        if (colors.Length==colors02.Length)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i]==colors02[i])
                {
                    samePixelQuantity++;
                }
            }
            if (colors02 != null) similarity = samePixelQuantity / colors02.Length;
            if (similarity >= similarityIndex)
            {
                if (prison!=null)
                {
                    prison.isUnlockingSucceeded = true;
                }

                if (outsideTheShukaku!=null)
                {
                    outsideTheShukaku.isUnlockingSucceeded = true;
                }
                
                draw.SetActive(false);
                GameManager.Instatic.AllowControl();
            }
            else
            {
                tipText.text = "描绘失败！";
                tip.GetComponent<TimedShutdown>().lengthOfTime = 2f;
                tip.SetActive(true);
            }
        }
    }

    public void Detection()
    {
        colors = texture2D.GetPixels();
        colors02 = targettexture2D.GetPixels();
        ContrastTexture2D();
    }
}
