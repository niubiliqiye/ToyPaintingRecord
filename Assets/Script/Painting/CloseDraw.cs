using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDraw : MonoBehaviour
{
    public new GameObject camera;
    //private bool isMouseEnter;
    public GameObject drawGameobject;
    
    private void Update()
    {
        //gameObject.GetComponent<PolygonCollider2D>().enabled = drawGameobject.activeSelf;
        
        if (gameObject.transform.position!=camera.transform.position)
        {
                    gameObject.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y,
                        gameObject.transform.position.z);
        }
        
        // if (!isMouseEnter&&Input.GetMouseButtonDown(0))
        // {
        //     isMouseEnter = false;
        //     drawGameobject.SetActive(false);
        // }
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     isMouseEnter = true;
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     isMouseEnter = false;
    // }
}
