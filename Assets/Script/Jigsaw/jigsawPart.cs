using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class jigsawPart : MonoBehaviour
{
    [Header("自身原始坐标")]public Vector3 oneSelfOriginalTransform;

    [Header("是否可以与其他物体交换")] public bool isSwap;
    
    [Header("是否是空缺位置")]public bool isVacancy;

    public JigsawManager jigsawManager;

    public Vector3 beforeMoving;
    // Start is called before the first frame update
    private void Awake()
    {
        oneSelfOriginalTransform = gameObject.transform.position;
    }

    // private void OnMouseOver()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (jigsawManager.firstTouch == null && jigsawManager.secondTouch == null&&!isSwap)
    //         {
    //             jigsawManager.firstTouch = gameObject;
    //         }
    //     }
    //     if(Input.GetMouseButtonUp(0))
    //     {
    //         if (jigsawManager.firstTouch != null && jigsawManager.secondTouch == null&&isSwap)
    //         {
    //             jigsawManager.secondTouch = gameObject;
    //         }
    //     }
    // }

    private void OnMouseDrag()
    {
        if (jigsawManager.firstTouch == null && jigsawManager.secondTouch == null&&!isSwap)
        {
            jigsawManager.firstTouch = gameObject;
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (jigsawManager.firstTouch != null && jigsawManager.secondTouch == null&&isSwap)
            {
                jigsawManager.secondTouch = gameObject;
            }
            else
            {
                jigsawManager.firstTouch = null;
            }
        }
    }
}
