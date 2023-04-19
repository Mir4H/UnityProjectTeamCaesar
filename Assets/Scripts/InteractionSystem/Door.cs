using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string doorSceneName;
    [SerializeField] private int doorLevel;

    [SerializeField] private bool canOpen = false;

    public int numberOfKeys = 0;

    public string InteractionPrompt => _prompt;
    

    public bool Interact(Player interactor)
    {
        //Tähän  mitä se interaktio tekee!!
        //var inventory = interactor.GetComponent<Inventory>();

        //if (inventory == null) return false;
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

        Debug.Log("No key found!");
        return false;
    }
}
