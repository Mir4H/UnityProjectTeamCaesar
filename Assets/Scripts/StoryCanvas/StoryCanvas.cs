using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StoryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject storyImage;
    [SerializeField] Button next;
    [SerializeField] Button prev;

    private void OnEnable()
    {
        EventManager.ShowStory += Storyrequested;
        EventManager.ShowOneStory += ShowOneStory;
    }

    private void Storyrequested()
    {
        storyImage.SetActive(true);
        Debug.Log("Many stories request");
    }

    private void OnDisable()
    {
        EventManager.ShowStory -= Storyrequested;
        EventManager.ShowOneStory -= ShowOneStory;
    }
    private void ShowOneStory()
    {
        storyImage.SetActive(true);
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        Debug.Log("Story request");
        Invoke("CloseThis", 11.0f);
    }
    private void CloseThis()
    {
        storyImage.SetActive(false);
    }
    public void Next()
    {
        StoryScript.currentlyShown++;
        Debug.Log("Next");
    }

    public void Previous()
    {
        StoryScript.currentlyShown--;
        Debug.Log("Previous");
    }

    public void Exit()
    {
        storyImage.SetActive(false);
       // Time.timeScale = 1;
    }
}