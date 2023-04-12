using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public Serializable<string, InventorySaveData> inventoryDictionary;

    public SaveData()
    {
        inventoryDictionary = new Serializable<string, InventorySaveData> ();
    }
}
