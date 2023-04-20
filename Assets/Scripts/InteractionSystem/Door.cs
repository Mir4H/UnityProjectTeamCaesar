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
}
