using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonEunuch : MonoBehaviour
{
    public GameObject dialogueUI;
    private void OnMouseDown()
    {
        if (!FindObjectOfType<Prison>().theThirdlySegmentIsOver&&!dialogueUI.activeSelf)
        {
            FindObjectOfType<Prison>().OpenDialogue(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        FindObjectOfType<Prison>().OpenDialogue(4);
    }
}
