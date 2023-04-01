using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockChapter : MonoBehaviour
{
    public SelectChapter[] chapters;
    /// <summary>
    /// 章节是否解锁
    /// </summary>
    public bool chapterUnlock1, chapterUnlock2, chapterUnlock3;


    private void Start()
    {
        ShowName();
    }

    private void ShowName()
    {
        if (chapterUnlock1)
        {
            chapters[0].SwitchPictures();
        }
        if (chapterUnlock2)
        {
            chapters[1].SwitchPictures();
        }
        if (chapterUnlock3)
        {
            chapters[2].SwitchPictures();
        }
    }
}
