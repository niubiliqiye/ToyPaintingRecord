using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Animation_Control : MonoBehaviour
{
    private SkeletonAnimation skeleton;
    public GameObject select;

    
    private void Awake()
    {
        skeleton = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        skeleton.AnimationName = "animation";

        Invoke("ShowAndClose",1.74f);
    }

    private void ShowAndClose()
    {
        gameObject.SetActive(false);
        select.SetActive(true);
    }
}
