using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Dossier : MonoBehaviour
{
    private Player player;
    private float distance;
    public float targetDistance;
    private ShuKaKu shuKaKu;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shuKaKu = FindObjectOfType<ShuKaKu>();
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }
    
    private void OnMouseDown()
    {
        if (distance<targetDistance&&shuKaKu.inTime)
        {
            shuKaKu.findDossier = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
