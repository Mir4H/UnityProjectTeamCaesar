using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public long lastUpdated;
    public string lastScene;
    public SerializableDictionary<string, bool> itemsCollected;
    public SerializableDictionary<string, bool> scenesCompleted;

   //Inventory saving
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public List<InventoryItem> inventoryItems;


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        // Here should be the player starting point value: -5.7, 0.2500001, -9.93
        playerPosition = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
        playerRotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);
        itemsCollected = new SerializableDictionary<string, bool>();
        scenesCompleted = new SerializableDictionary<string, bool>();

        //Inventory saving
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        inventoryItems = new List<InventoryItem>();

    }
}
