using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class Install : MonoBehaviour
{
    public GameObject install;
    public  GameObject volume; 
    private SkeletonGraphic skeleton;

    private void Awake()
    {
        skeleton = install.GetComponent<SkeletonGraphic>();
    }


    // Update is called once per frame
    void Update()
    {
        if (install.activeSelf && Input.GetMouseButtonDown(0)&& !FindObjectOfType<Volume>().isEnter)
        {
            Install_Close();
            GameManager.Instatic.AllowControl(1);
        }
    }
    
    public void Install_Open()
    {
        skeleton.AnimationState.SetAnimation(0, "open", false);
        Invoke("Open",0.7f);
    }
    
    public void Install_Close()
    {
        skeleton.AnimationState.SetAnimation(0,"close", false);
        volume.gameObject.SetActive(false);
        Invoke("Close",0.7f);
    }

    private void Open()
    {
        GameManager.Instatic.openMenu = true;
        volume.gameObject.SetActive(true);
    }
    
    private void Close()
    {
        install.SetActive(false);
        GameManager.Instatic.openMenu = false;
    }
}
