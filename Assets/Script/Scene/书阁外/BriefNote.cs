using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class BriefNote : MonoBehaviour
{
    public float minDistance=5.5f;
    private GameObject player;
    private OutsideTheShukaku outsideTheShukaku;
    private float distance;

    private void Awake()
    {
        outsideTheShukaku = FindObjectOfType<OutsideTheShukaku>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    private void OnMouseDown()
    {
        if (distance<=minDistance)
        {

            outsideTheShukaku.clickBriefNote = true;
        }
    }
    
    
}
