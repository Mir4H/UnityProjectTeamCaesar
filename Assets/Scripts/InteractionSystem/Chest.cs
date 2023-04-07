using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        //T�h�n  mit� se interaktio tekee!!

        Debug.Log("Opening Chest!");
        return true;
    }
}
