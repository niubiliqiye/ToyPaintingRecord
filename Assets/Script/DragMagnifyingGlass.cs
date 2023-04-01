using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DragMagnifyingGlass : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 dragOffset;
    public Camera mainCamera;

    public Transform small;
    public Transform big;
    public Transform spriteMaskTransform;

    private void OnMouseDown()
    {
        dragOffset = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition - dragOffset;
    }

    private void Update()
    {
        big.position = small.position * 2 - spriteMaskTransform.position;
    }
}
