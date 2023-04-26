using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StartSceneCanvas : MonoBehaviour
{
    [SerializeField] private GameObject startStory;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject image2;
    [SerializeField] private InventorySystem inventory;

    private void OnEnable()
    {
        image.SetActive(true);
        image2.SetActive(false);
        Invoke("ChangeImage", 15f);
    }

    private void ChangeImage()
    {
        image.SetActive(false);
        image2.SetActive(true);
        Invoke("Close", 15f);
    }

    public void Close()
    {
        startStory.SetActive(false);
    }

}