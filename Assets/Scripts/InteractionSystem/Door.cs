using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string doorSceneName;
    [SerializeField] private int doorLevel;
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private int keyID;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Player player;

    private bool canOpen = false;

    private int numberOfKeys;

    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        foreach (InventoryItem item in inventory.Container.Items)
        {
            if (item.ID == keyID)
            {
                numberOfKeys = item.StackSize;
            }
        }

        if (numberOfKeys >= doorLevel)
        {
            canOpen = true;
        }

        if (canOpen)
        {
            SceneManager.LoadSceneAsync(doorSceneName);
            SetPlayerPosition(doorSceneName);
            DataPersistenceManager.instance.SetNewLevel(true);
            DataPersistenceManager.instance.SaveGame();
            Debug.Log("Opening Door!");
            return true;
        }
        else
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp($"You need {doorLevel} keys for this door!");
        }

        Debug.Log("No key found!");
        return false;
    }

    public void SetPlayerPosition(string sceneName)
    {
        if (sceneName == "Tutorial") player.transform.position = new Vector3((float)-5.69999981, (float)0.25000006, (float)-9.93000031);
        if (sceneName == "Sokkelo") player.transform.position = new Vector3(62, 0, -14); Debug.Log(sceneName);
        if (sceneName == "VipuScene") player.transform.position = new Vector3(62, 0, -13); Debug.Log(sceneName);
        if (sceneName == "DarkRoom") player.transform.position = new Vector3(-10, 0, -14); Debug.Log(sceneName);
    }
}
