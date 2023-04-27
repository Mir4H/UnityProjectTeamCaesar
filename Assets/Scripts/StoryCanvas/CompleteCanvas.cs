using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CompleteCanvas : MonoBehaviour
{
    [SerializeField] private GameObject completed;
    void Start()
    {
        Invoke("Complete", 1f);
    }

    void Complete()
    {
        completed.SetActive(true);
        Invoke("MainMenu", 16f);
    }

    void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}