using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("摄像机视角最大值")] public float sizeMax;
    [Header("摄像机视角最小值")] public float sizeMin;
    [Header("摄像机移动速度")] public float cameraMoveSpeed = 100;
    [Header("摄像机缩放速度")] public float cameraZoomSpeed = 200;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            CameraOrthographicSiz();
        }
        else
        {
            CameraMove();
        }
    }


    public void CameraOrthographicSiz()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraMoveSpeed;

        if (cam.orthographicSize >= sizeMin && cam.orthographicSize <= sizeMax)
            cam.orthographicSize += wheel;
        else
        {
            if (cam.orthographicSize < 3)
            {
                cam.orthographicSize = 3;
            }

            if (cam.orthographicSize > 5)
            {
                cam.orthographicSize = 5;
            }
        }
    }

    public void CameraMove()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //隐藏鼠标
            //Cursor.visible = false;
            float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraMoveSpeed;
            cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x,
                cam.gameObject.transform.position.y + wheel,
                cam.gameObject.transform.position.z);
        }
        else
        {
            //显示鼠标
            //Cursor.visible = true;
            float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100;
            cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x + wheel,
                cam.gameObject.transform.position.y,
                cam.gameObject.transform.position.z);
        }
    }
}