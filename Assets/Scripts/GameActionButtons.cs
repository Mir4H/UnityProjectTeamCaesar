using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameActionButtons : MonoBehaviour
{
    [Header("MainMenuButtons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Tallennushommien testaus, jos ei toimi, ota käyttöön yläpuoli!

        DisableMenuButtons();
        // Create a new game - which will intialize our game data
        DataPersistenceManager.instance.NewGame();
        //Load the gameplay scene - which will in turn save the game because of OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("InteractScene");
    }

    public void Continue()
    {
        DisableMenuButtons();
        // Load the next scene - which will in turn loat the game because of OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("InteractScene");
        Debug.Log("Continue from the last save");
    }

    public void PlayGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Replay()
    {
        SceneManager.LoadScene("SceneZero");
    }

    public void Options()
    {
        DisableMenuButtons();
        Debug.Log("Opening options");
    }

    public void Quit()
    {
        DisableMenuButtons();
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Disabled all the buttons, meant to be used first after click to prevent from double clicks
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        quitButton.interactable = false;
        optionsButton.interactable = false;
    }

}
