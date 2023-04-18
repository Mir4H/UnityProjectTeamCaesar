using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private bool hasKey;

    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        //Tähän  mitä se interaktio tekee!!
        //var inventory = interactor.GetComponent<Inventory>();

        //if (inventory == null) return false;

        if (hasKey)
        {
            Debug.Log("Opening Door!");
            return true;
        }

        Debug.Log("No key found!");
        return false;
    }
}
