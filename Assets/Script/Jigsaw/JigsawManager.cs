using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class JigsawManager : MonoBehaviour
{
    [Header("拼图零件")] public jigsawPart[] jigsawPart;
    [Header("交换速度")] public float speed=5;
    public GameObject reserve;
    public GameObject firstTouch;
    public GameObject secondTouch;
    
    /// <summary>
    /// 两个拼图是否可以交换
    /// </summary>
    private bool isCanBeExchanged;

    /// <summary>
    /// 存放初始位置
    /// </summary>
    private Vector3[] oneSelfOriginalTransform;
    
    /// <summary>
    /// 存放空缺位置的编号
    /// </summary>
    private int _vacancyIndex;

    private ShuKaKu shuKaKu;

    public Transform mainCamear;

    public bool accomplish;
    // Start is called before the first frame update
    private void Awake()
    {
        oneSelfOriginalTransform = new Vector3[9];
        shuKaKu = FindObjectOfType<ShuKaKu>();
    }

    private void Update()
    {
        if (gameObject.transform.parent.transform.position!=new Vector3(mainCamear.position.x, mainCamear.position.y,
                gameObject.transform.parent.transform.position.z))
        {
            gameObject.transform.parent.transform.position = new Vector3(mainCamear.position.x, mainCamear.position.y,
                gameObject.transform.parent.transform.position.z);
        }

    }

    void Start()
    {
        for (int i = 0; i < jigsawPart.Length; i++)
        {
            oneSelfOriginalTransform[i] = jigsawPart[i].oneSelfOriginalTransform;
        }
        _vacancyIndex=Random.Range(0, 9);
        jigsawPart[_vacancyIndex].isVacancy = true;
        jigsawPart[_vacancyIndex].gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        jigsawPart[_vacancyIndex].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        jigsawPart[_vacancyIndex].gameObject.GetComponent<BoxCollider2D>().enabled = false;
        reserve.SetActive(true);
        reserve.transform.position = jigsawPart[_vacancyIndex].gameObject.transform.position;
        jigsawPart[_vacancyIndex].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Disorder();
        Disorder();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Exchange();
    }

    /// <summary>
    /// 将拼图的顺序打乱
    /// </summary>
    void Disorder()
    {
        for (int i = 0; i < jigsawPart.Length; i++)
        {
            var temp = jigsawPart[i];
            var tempIndex=Random.Range(0, 9);
            if (i == _vacancyIndex || tempIndex == _vacancyIndex)
            {
                continue;
            }
            jigsawPart[i] = jigsawPart[tempIndex];
            jigsawPart[tempIndex]= temp;
        }
        for (int i = 0; i < jigsawPart.Length; i++)
        {
            jigsawPart[i].transform.position = oneSelfOriginalTransform[i];
        }
    }

    /// <summary>
    /// 交换位置
    /// </summary>
    void Exchange()
    {
        if (firstTouch != null && secondTouch != null)
        {
            if (secondTouch.GetComponent<jigsawPart>().isSwap)
            {
                if (firstTouch.GetComponent<jigsawPart>().beforeMoving == Vector3.zero&&secondTouch.GetComponent<jigsawPart>().beforeMoving==Vector3.zero)
                {
                    firstTouch.GetComponent<jigsawPart>().beforeMoving = firstTouch.transform.position;
                    secondTouch.GetComponent<jigsawPart>().beforeMoving = secondTouch.transform.position;
                    
                }
                IsCanBeExchanged();
                if (isCanBeExchanged)
                {
                    firstTouch.transform.position = Vector3.MoveTowards(firstTouch.transform.position,
                        reserve.GetComponent<jigsawPart>().beforeMoving, Time.deltaTime * speed);
                    if (firstTouch.transform.position == secondTouch.GetComponent<jigsawPart>().beforeMoving)
                    {
                        secondTouch.transform.position = firstTouch.GetComponent<jigsawPart>().beforeMoving;
                        firstTouch.GetComponent<jigsawPart>().beforeMoving = Vector3.zero;
                        secondTouch.GetComponent<jigsawPart>().beforeMoving = Vector3.zero;
                        firstTouch = null;
                        secondTouch = null;
                        isCanBeExchanged = false;
                    }

                    if (IsComplete()||accomplish)
                    {
                        shuKaKu.jigsawAccomplish = true;
                        FindObjectOfType<Player>().gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        //gameObject.transform.parent.gameObject.SetActive(false);
                    }
                }
                else
                {
                    firstTouch.GetComponent<jigsawPart>().beforeMoving = Vector3.zero;
                    secondTouch.GetComponent<jigsawPart>().beforeMoving = Vector3.zero;
                    firstTouch = null;
                    secondTouch = null;
                    isCanBeExchanged = false;
                }

            }
        }
    }

    /// <summary>
    /// 判断是否可以交换
    /// </summary>
    void IsCanBeExchanged()
    {
        var positionX = secondTouch.GetComponent<jigsawPart>().beforeMoving.x - firstTouch.GetComponent<jigsawPart>().beforeMoving.x;
        var positionY = secondTouch.GetComponent<jigsawPart>().beforeMoving.y - firstTouch.GetComponent<jigsawPart>().beforeMoving.y;
        if ((positionX == 0 && Mathf.Abs(positionY) == 2) || positionY == 0 && Mathf.Abs(positionX) == 2)
        {
            isCanBeExchanged = true;
        }
    }

    /// <summary>
    /// 检测是否拼完
    /// </summary>
    /// <returns></returns>
    public bool IsComplete()
    {
        for (int i = 0; i < jigsawPart.Length; i++)
        {
            if (jigsawPart[i].transform.position != jigsawPart[i].oneSelfOriginalTransform)
            {
                return false;
            }
        }

        return true;
    }
}
