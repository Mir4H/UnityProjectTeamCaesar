using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<string> collectedItems;
    public Serializable<string, ItemPickUpSaveData> activeItems;

    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new Serializable<string, ItemPickUpSaveData>();
    }
}
