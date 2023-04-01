using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Volume : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] volumes;
    public bool isEnter;
    public float volume=1;
    private AudioSource audioSource;

    private void Awake()
    {
        //volumes = GameManager.Instatic.volume
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        volume = GameManager.Instatic.volume;
        switch (volume)
        {
            case 0:
                Zero();
                break;
            case 0.2f:
                First();
                break;
            case 0.4f:
                Second();
                break;
            case 0.6f:
                Thirdly();
                break;
            case 0.8f:
                Fourthly();
                break;
            case 1f:
                Fifth();
                break;
        }
    }

    // private void Start()
    // {
    //     switch (volume)
    //     {
    //         case 0:
    //             Zero();
    //             break;
    //         case 0.2f:
    //             First();
    //             break;
    //         case 0.4f:
    //             Second();
    //             break;
    //         case 0.6f:
    //             Thirdly();
    //             break;
    //         case 0.8f:
    //             Fourthly();
    //             break;
    //         case 1f:
    //             Fifth();
    //             break;
    //     }
    // }

    public void Zero()
    {
        volumes[0].SetActive(false);
        volumes[1].SetActive(false);
        volumes[2].SetActive(false);
        volumes[3].SetActive(false);
        volumes[4].SetActive(false);
        volume = 0;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void First()
    {
        volumes[0].SetActive(true);
        volumes[1].SetActive(false);
        volumes[2].SetActive(false);
        volumes[3].SetActive(false);
        volumes[4].SetActive(false);
        volume = 0.2f;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void Second()
    {
        volumes[0].SetActive(true);
        volumes[1].SetActive(true);
        volumes[2].SetActive(false);
        volumes[3].SetActive(false);
        volumes[4].SetActive(false);
        volume = 0.4f;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void Thirdly()
    {
        volumes[0].SetActive(true);
        volumes[1].SetActive(true);
        volumes[2].SetActive(true);
        volumes[3].SetActive(false);
        volumes[4].SetActive(false);
        volume =0.6f;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void Fourthly()
    {
        volumes[0].SetActive(true);
        volumes[1].SetActive(true);
        volumes[2].SetActive(true);
        volumes[3].SetActive(true);
        volumes[4].SetActive(false);
        volume = 0.8f;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void Fifth()
    {
        volumes[0].SetActive(true);
        volumes[1].SetActive(true);
        volumes[2].SetActive(true);
        volumes[3].SetActive(true);
        volumes[4].SetActive(true);
        volume = 1f;
        //TODO:音量大小调节
        audioSource.volume = volume;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }
}
