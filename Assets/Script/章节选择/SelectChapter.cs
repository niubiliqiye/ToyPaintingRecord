using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChapter : MonoBehaviour
{
    //public bool isHiddenName;
    private SpriteRenderer[] spriteRenderers;
    public Sprite[] sprites;

    private void Awake()
    {
        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        //TODO:鼠标进入事件
        print("鼠标进入物体");
    }

    private void OnMouseExit()
    {
        //TODO:鼠标离开事件
        print("鼠标离开物体");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TODO:选中章节事件
            Debug.LogWarning(gameObject.name);
        }
    }

    public void SwitchPictures()
    {
        for (int i = 1; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = sprites[i-1];
        }
    }
}
