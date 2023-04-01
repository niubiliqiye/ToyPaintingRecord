using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class KeyCylinder : MonoBehaviour
{
    private TextMeshProUGUI numberIndex;
    public int index;

    private void Awake()
    {
        numberIndex = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        index = Random.Range(1, 9);
        numberIndex.text = index.ToString();
    }

    public void Up()
    {
        index++;
        if (index==10)
        {
            index = 1;
        }
        numberIndex.text = index.ToString();
        GetComponentInParent<Lock>().UnLock();
    }

    public void Down()
    {
        index--;
        if (index==0)
        {
            index = 9;
        }
        numberIndex.text = index.ToString();
        GetComponentInParent<Lock>().UnLock();
    }
}
