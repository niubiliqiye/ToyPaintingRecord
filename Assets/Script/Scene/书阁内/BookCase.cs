using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCase : MonoBehaviour
{
    private ShuKaKu shuKaKu;
    private float distance;
    private GameObject player;

    private void Awake()
    {
        shuKaKu = FindObjectOfType<ShuKaKu>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    private void OnMouseDown()
    {
        if (distance<shuKaKu.distance)
        {
            shuKaKu.onClickBookCase = true;
        }
    }
}
