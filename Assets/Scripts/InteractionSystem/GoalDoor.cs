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
    [SerializeField] private GameObject keyToFind;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Player player;
    [SerializeField] private bool puzzleSolved;

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

    public bool Interact(Player interactor)
    {
        if (this.gameObject.tag == "goal")
        {
            //T‰‰ tarkastaa
            if (keyToFind == null)
            {
                Debug.Log("Pass");

                playedScene = SceneManager.GetActiveScene().name;

                SceneManager.LoadSceneAsync(doorSceneName);

                player.transform.position = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
                player.transform.rotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);

                DataPersistenceManager.instance.SaveGame();

                //OnTimerStop();

                return true;
            }
            else
            {
                Debug.Log("Need a key");
            }
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
