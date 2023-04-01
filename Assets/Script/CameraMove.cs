using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
{
    [Header("相机最小的坐标")]public float minPosition;

    [Header("相机最大的坐标")]public float maxPosition;

    [Header("跟随物体")] public GameObject player;

    [Header("跟随速度")] public float moveSpeed=5;

    [Header("摄像机跟随模式")]public bool movePattern;
    
    private string mouseXString = "Mouse X";


    public float sensitivityDrag = 0.5f;

    private SpriteRenderer mouse;

    private void Awake()
    {
        mouse = FindObjectOfType<MouseMove>().gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (movePattern)
        {
            Move02();
        }
    }

    void FixedUpdate()
    {
        if (!movePattern)
        {
            Move();
        }
    }

    void Move()
    {
        var position = gameObject.transform.position;
        if (player.transform.position.x >= minPosition && player.transform.position.x <= maxPosition)
        {
            position = Vector3.Lerp(position, new Vector3(player.transform.position.x, position.y, position.z),
                Time.deltaTime * moveSpeed);
        }
        else
        {
            if (player.transform.position.x < minPosition)
            {
                position = Vector3.Lerp(position, new Vector3(minPosition, position.y, position.z),
                    Time.deltaTime * moveSpeed);
            }

            if (player.transform.position.x > maxPosition)
            {
                position = Vector3.Lerp(position, new Vector3(maxPosition, position.y, position.z),
                    Time.deltaTime * moveSpeed);
            }
        }
        gameObject.transform.position = position;
    }

    /// <summary>
    /// 鼠标右键拖拽摄像机
    /// </summary>
    private void Move02()
    {
        if (gameObject.transform.position.x>=minPosition&&gameObject.transform.position.x<=maxPosition)
        {
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x + wheel.y, gameObject.transform.position.y, gameObject.transform.position.z);
            if (Input.GetMouseButton(1))
            {
                if (mouse.enabled)
                {
                    mouse.enabled=false;
                }
                Vector3 p1 = gameObject.transform.position - gameObject.transform.right * Input.GetAxisRaw(mouseXString) *
                    sensitivityDrag * Time.timeScale;
                gameObject.transform.position = p1;
            }
            else
            {
                if (!mouse.enabled)
                {
                    mouse.enabled=true;
                }
            }
        }
        else
        {
            var position = gameObject.transform.position;
            if (gameObject.transform.position.x < minPosition)
            {
                position = Vector3.Lerp(position, new Vector3(minPosition, position.y, position.z),
                    Time.deltaTime * moveSpeed);
            }

            if (gameObject.transform.position.x > maxPosition)
            {
                position = Vector3.Lerp(position, new Vector3(maxPosition, position.y, position.z),
                    Time.deltaTime * moveSpeed);
            }
            gameObject.transform.position = position;
        }
    }

    private void Lessen()
    {
        gameObject.GetComponent<Camera>().orthographicSize =
            Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, 3.5f, Time.deltaTime);
    }
}
