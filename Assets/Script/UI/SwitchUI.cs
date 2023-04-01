using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUI : MonoBehaviour
{
    public NPCUI npcUI;

    private void OnMouseOver()
    {
        npcUI.UIInterface = gameObject.name;
        if (Input.GetMouseButtonDown(0))
        {
            //print(2);
            npcUI.SwitchCloseInterface();
        }
    }

    private void OnMouseExit()
    {
        npcUI.UIInterface = "";
    }
}
