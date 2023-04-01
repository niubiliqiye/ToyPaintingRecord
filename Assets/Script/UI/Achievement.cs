using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Achievement : MonoBehaviour
{
    public GameObject achievement;
    private SkeletonGraphic skeleton;

    /// <summary>
    /// 已解锁成就数量
    /// </summary>
    private int Achievement_Number;

    private string openName;
    private string idleName;
    private string closeName;
    // Start is called before the first frame update
    private void Awake()
    {
        skeleton = achievement.GetComponent<SkeletonGraphic>();
    }

    void Start()
    {
        Achievement_Number = GameManager.Instatic.unlockAchievementNumber;
        openName = "open-" + Achievement_Number;
        idleName = "idle-" + Achievement_Number;
        closeName = "close-" + Achievement_Number;
        //skeleton.AnimationState.SetAnimation(0, openName, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (achievement.activeSelf && Input.GetMouseButtonDown(0))
        {
            Achievement_Close();
        }
    }

    public void Achievement_Open()
    {
        skeleton.AnimationState.SetAnimation(0, openName, false);
        Invoke("Achievement_Idle",0.7f);
    }

    public void Achievement_Idle()
    {
        skeleton.AnimationState.SetAnimation(0, idleName, true);
        
    }

    public void Achievement_Close()
    {
        skeleton.AnimationState.SetAnimation(0, closeName, false);
        Invoke("Close",0.7f);
    }

    private void Close()
    {
        achievement.SetActive(false);
    }
}
