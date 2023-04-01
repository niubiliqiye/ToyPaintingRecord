using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
//public class EventVector2:UnityEvent<Vector2>{}
public class MouseManager : MonoBehaviour
{
    //public EventVector2 OnMouseClicked;

    private RaycastHit2D hit;
    public GameObject prefab;

    [Header("鼠标指针图片")] [Tooltip("point:默认鼠标样式  move:移动鼠标样式")]
    public Texture2D point, move;

    private void Update()
    {
        /*SetCurSorTexture();
        if (InteractWithUI()) return;
        MouseControl();*/
        
    }

    void SetCurSorTexture()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, Vector2.zero);
            //Debug.DrawRay(ray.origin, ray.direction * 20f, Color.blue, duration: 0.5f);
        }

        //Debug.Log(hit.collider.gameObject.name);
        if (hit.collider != null)
        {
            //切换鼠标贴图
            switch (hit.collider.gameObject.tag)
            {
                case "Ground":
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        Cursor.SetCursor(move, new Vector2(16,16), CursorMode.Auto);
                    }
                    else
                    {
                        Cursor.SetCursor(point, Vector2.zero, CursorMode.Auto);
                    }

                    break;
                default:
                    Cursor.SetCursor(point, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                //GameManager.Instatic.isGround = true;
            }
        }
    }

    bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GeneratingObject()
    {
        //GameObject go=GameObject.Instantiate(prefab,Camera.main.ScreenToWorldPoint(Input.mousePosition),)
    }
}