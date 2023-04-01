using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    public GameObject[] MouseSpecialOfficiency;
    public bool isMap;
    void Update()
    {
        MouseFollow();
        ShowMouse();
        if (!isMap)
        {
            ShowMouseSpecialOfficiency(0);
        }
        else
        {
            ShowMouseSpecialOfficiency(1);
        }

    }
    
    /// <summary>
    /// 鼠标UI跟随鼠标
    /// </summary>
    private void MouseFollow()
    {
        //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        transform.position = mousePositionInWorld;
    }

    private void ShowMouse()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    private void ShowMouseSpecialOfficiency(int Index)
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseSpecialOfficiency[Index].SetActive(false);
            MouseSpecialOfficiency[Index].SetActive(true);
            //Invoke("CloseMouseSpecialOfficiency",4f);
        }
        
    }

    private void CloseMouseSpecialOfficiency(int Index)
    {
        MouseSpecialOfficiency[Index].SetActive(false);
    }
}
