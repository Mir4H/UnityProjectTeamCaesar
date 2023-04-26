using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StoryCanvas : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject storyImage;
    [SerializeField] Button next;
    [SerializeField] Button prev;

    [SerializeField] private GameObject diary;
    [SerializeField] private GameObject diaryText;
    [SerializeField] private GameObject diaryText2;
    [SerializeField] private GameObject diaryText3;
    [SerializeField] private GameObject diaryImage;
    [SerializeField] private bool diaryDecryped = false;

    private void OnEnable()
    {
        EventManager.ShowStory += Storyrequested;
        EventManager.ShowOneStory += ShowOneStory;
        EventManager.ShowDiary += OnShowDiary;
        EventManager.DiaryDecrypted += OnDiaryDecrypted;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDiaryDecrypted()
    {
        diaryDecryped = true;
        OnShowDiary();
    }

    public void SaveData(GameData data)
    {
        data.diaryDecryped = diaryDecryped;
    }

    public void LoadData(GameData data)
    {
        diaryDecryped = data.diaryDecryped;
    }

    private void OnShowDiary()
    {
        diary.SetActive(true);
        if (!diaryDecryped)
        {
            diaryText.SetActive(true);
            diaryText2.SetActive(false);
            diaryText3.SetActive(false);
            diaryImage.SetActive(false);
            Invoke("Exit", 12f);
        }
        else
        {
            diaryText.SetActive(false);
            diaryText2.SetActive(true);
            diaryText3.SetActive(false);
            diaryImage.SetActive(true);
            Invoke("NextPage", 25f);
        }
    }

    private void NextPage()
    {
        diaryText.SetActive(false);
        diaryText2.SetActive(false);
        diaryText3.SetActive(true);
        Invoke("Exit", 44f);
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
        EventManager.ShowDiary -= OnShowDiary;
        EventManager.DiaryDecrypted -= OnDiaryDecrypted;
    }
    private void ShowOneStory()
    {
        storyImage.SetActive(true);
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        Debug.Log("Story request");
        Invoke("CloseThis", 31.0f);
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
        diary.SetActive(false);
        CancelInvoke();
        // Time.timeScale = 1;
    }
}