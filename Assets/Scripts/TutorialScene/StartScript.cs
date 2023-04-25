using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject startstory;
    private bool storyShown = false;

    private void Start()
    {
         Invoke("Story", 2f);
    }
    
    private void Story()
    {
        if (!storyShown)
        {
            startstory.SetActive(true);
            storyShown = true;
        }
    }

    public void SaveData(GameData data)
    {
        data.firstStory = storyShown;
    }

    public void LoadData(GameData data)
    {
        storyShown = data.firstStory;
    }
}
