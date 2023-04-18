using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IDataPersistence
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Pause Menu Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button saveGameButton;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private string currentSceneName;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Getting current scene name
    public void LoadData(GameData data)
    {
        currentSceneName = data.currentScene;
    }

    // No need to save anything
    public void SaveData(GameData data)
    {
        //nothing
    }

    public void SaveGame()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void Continue()
    {
        DisableMenuButtons();
        // Save the game anytime before a new scene
        // Load the scene where player was when pressing esc - which will in turn load the game because of OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(currentSceneName);
        Debug.Log("Continue from the last save");
    }

    public void RestartLevel()
    {
        DisableMenuButtons();
        Debug.Log("Restarting level");
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
        saveGameButton.interactable = false;
        restartLevelButton.interactable = false;
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
