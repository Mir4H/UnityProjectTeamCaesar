using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class Door : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private string _prompt;
    [SerializeField] private string doorSceneName;
    [SerializeField] private int doorLevel;
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private int keyID;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Player player;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private int requiredAmount;

    private bool canOpen = false;

    private int numberOfKeys;

    private SerializableDictionary<string, bool> _scenesCompleted;

    public string InteractionPrompt => _prompt;

    public void LoadData(GameData data)
    {
         _scenesCompleted = data.scenesCompleted;
    }

    public void SaveData(GameData data)
    {
        //
    }

    private void Start()
    {
        if (doorSceneName == "FinalScene")
        {
            _prompt = "You are about to enter the final Challenge! \n" +
                "Press E to Open";
        }
    }

    public bool Interact(Player interactor)
    {
        bool sceneCompleted = false;

        if (_scenesCompleted != null)
        {
            foreach (KeyValuePair<string, bool> entry in _scenesCompleted)
            {
                if (entry.Key == doorSceneName)
                {
                    sceneCompleted = true;
                }
            }
        }

        if (doorSceneName == "UnderConstruction")
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp("This room is still under construction. Come back on final version!");
            return false;
        } 
        else if (sceneCompleted) 
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp("You have already completed this challenge!");
            return false;
        }
        else 
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
                if (doorSceneName == "FinalScene")
                {
                    var items = inventory.Container.Items.Find(x => x.ID == 1);
                    var nroOfItems = 0;
                    if (items != null)
                    {
                        Debug.Log(items.StackSize);
                        nroOfItems = items.StackSize;
                    }
                    if (nroOfItems < requiredAmount)
                    {
                        string secondaryPrompt = $"Find {requiredAmount - nroOfItems} diary pages to open.";
                        if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                        interactionPromptUI.SetUp(secondaryPrompt);
                        return false;
                    }
                    else
                    {
                        canOpen = true;
                    }
                }
                else
                {
                    canOpen = true;
                }
            }

            if (canOpen)
            {
                Debug.Log("Opening Door!");
                DataPersistenceManager.instance.SetNewLevel(true);
                DataPersistenceManager.instance.SaveGame();
                //SceneManager.LoadSceneAsync(doorSceneName);
                LoadScene();
                SetPlayerPosition(doorSceneName);
                DataPersistenceManager.instance.SaveGame();
                return true;
            }
            else
            {
                if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                interactionPromptUI.SetUp($"You need {doorLevel} keys for this door!");
                return false;
            }

            Debug.Log("No key found!");
            return false;
        }
    }

    public void SetPlayerPosition(string sceneName)
    {
        if (sceneName == "Tutorial") player.transform.position = new Vector3(-5.69999981f, 0.25000006f, -9.93000031f);
        if (sceneName == "Sokkelo") player.transform.position = new Vector3(62, 0, -14);
        if (sceneName == "VipuScene") player.transform.position = new Vector3(20.0799999f, 0, -12.5600004f);
        if (sceneName == "DarkRoom") player.transform.position = new Vector3(-6.32999992f, 0, -14);
        if (sceneName == "FinalScene") player.transform.position = new Vector3(-5.69999981f, 0.25000006f, -13.4399996f);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(doorSceneName));
    }

    IEnumerator LoadSceneAsync(string doorSceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(doorSceneName);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
