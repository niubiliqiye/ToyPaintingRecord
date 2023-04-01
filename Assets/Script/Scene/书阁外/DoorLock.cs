using System;
using System.Collections;
using System.Collections.Generic;
using Script.Inventory.Item.ScriptableObject;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public float minDistance=3f;
    private GameObject player;
    private OutsideTheShukaku outsideTheShukaku;
    private float distance;
    private void Awake()
    {
        player = GameObject.Find("Player");
        outsideTheShukaku = FindObjectOfType<OutsideTheShukaku>();
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    private void OnMouseDown()
    {
        if (distance<=minDistance)
        {
            outsideTheShukaku.clickDoorLock = true;
        }
    }
}
