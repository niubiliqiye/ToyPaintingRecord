using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Archive : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;

    public GameObject archive;

    /// <summary>
    /// 打开存档动画，切换存档动画
    /// </summary>
    public void Open()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "open", false);
        skeletonGraphic.gameObject.SetActive(true);
        Invoke("OpenArchive",0.268f);
    }

    /// <summary>
    /// 打开存档界面
    /// </summary>
    private void OpenArchive()
    {
        archive.SetActive(true);
    }

    /// <summary>
    /// 关闭存档界面，切换存档动画
    /// </summary>
    public void Close()
    {
        archive.SetActive(false);
        skeletonGraphic.AnimationState.SetAnimation(0, "close", false);
        Invoke("CloseAnimator",0.268f);
    }

    /// <summary>
    /// 关闭存档动画
    /// </summary>
    private void CloseAnimator(){
        skeletonGraphic.gameObject.SetActive(false);
    }
}
