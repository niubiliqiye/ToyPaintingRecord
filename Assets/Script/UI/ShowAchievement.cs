using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchievement : MonoBehaviour
{
    public GameObject[] achievementsIcon;

    private int index;

    /// <summary>
    /// 显示成就
    /// </summary>
    public void Show(int index)
    {
        //TODO:解锁对应成就
        this.index = index;
        achievementsIcon[this.index].SetActive(true);
        Invoke("Close", 2f);
    }

    /// <summary>
    /// 关闭成就
    /// </summary>
    public void Close()
    {
        achievementsIcon[index].SetActive(false);
    }
}
