using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.gameObject.SetActive(false);
            loadGameButton.gameObject.SetActive(false);
        }
    }

    public void NewGame()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void LoadGame()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void Continue()
    {
        DisableMenuButtons();
        // Save the game anytime before a new scene
        DataPersistenceManager.instance.SaveGame();
        // Load the next scene - which will in turn loat the game because of OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("SaveTestScene");
        Debug.Log("Continue from the last save");
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

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
