using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class GoalDoor : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private string _prompt;
    [SerializeField] private string doorSceneName;
    [SerializeField] private int doorLevel;
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private int keyID;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private bool puzzleSolved = false;

    private int numberOfKeys;
    private string playedScene;

    public string InteractionPrompt => _prompt;

    public void SaveData(GameData data)
    {
        if (playedScene != null)
        {
            data.scenesCompleted.Add(playedScene, true);
        }
    }

    public void LoadData(GameData data)
    {
        //
    }

    public int GetNumOfKeys ()
    {
        numberOfKeys = 0;
        foreach (InventoryItem item in inventory.Container.Items)
        {
            if (item.ID == keyID)
            {
                numberOfKeys = item.StackSize;
            }
        }
        return numberOfKeys;
    }

    public bool Interact(Player interactor)
    {
        if (puzzleSolved)
        {
            GetNumOfKeys();

            if (numberOfKeys < doorLevel)
            {
                // lisää avain inventoryyn
                Debug.Log("Adding key to inventory!");
            }
            else
            {
                // lisää aikajuoma inventoryyn
                Debug.Log("Adding timepotion to inventory!");
            }

            playedScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadSceneAsync(doorSceneName);

            transform.position = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
            transform.rotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);

            // Ilmoitus uudesta esineestä??

            DataPersistenceManager.instance.SaveGame();

            //OnTimerStop();
            
            return true;
        }
        else
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp($"You need to solve the puzzle to opent this door!");
        }

        Debug.Log("Puzzle not solved yet!");
        return false;
    }
}
