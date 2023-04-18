using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject storyImage;
    private Button selectedItem;

    private void OnEnable()
    {
        EventManager.ShowStory += Storyrequested;
    }

    private void Storyrequested()
    {
        storyImage.SetActive(true);
        Debug.Log("Story request");
    }

    private void OnDisable()
    {
        EventManager.ShowStory -= Storyrequested;
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
    }
}