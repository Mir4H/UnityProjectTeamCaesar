using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ItemObject : MonoBehaviour
{
    private InventoryItemData referenceItem;
    private string uniqueId;
    private ItemPickUpSaveData itemSaveData;

    public void Awake()
    {
        uniqueId = GetComponent<UniqueID>().ID;
        referenceItem = GetComponent<ItemCollectable>().item;
        SaveLoad.OnLoadInventory += LoadInventory;
        itemSaveData = new ItemPickUpSaveData(referenceItem.Id, transform.position, transform.rotation);
    }

    private void Start()
    {
        Debug.Log(uniqueId);
        SaveInventoryManager.data.activeItems.Add(uniqueId, itemSaveData);
    }

    private void LoadInventory(SaveData data)
    {
        if (data.collectedItems.Contains(uniqueId)) Destroy(this.gameObject);
        if (data.activeItems.ContainsKey(uniqueId)) {
            gameObject.transform.SetPositionAndRotation(data.activeItems[uniqueId].position, data.activeItems[uniqueId].rotation);
        }
    }

    private void OnDestroy()
    {
        if (SaveInventoryManager.data.activeItems.ContainsKey(uniqueId)) SaveInventoryManager.data.activeItems.Remove(uniqueId);
        SaveLoad.OnLoadInventory -= LoadInventory;
    }

    public void OnHandlePickupItem()
    {
        Debug.Log("I collected it");
        SaveInventoryManager.data.collectedItems.Add(uniqueId);
        Destroy(gameObject);
    }
    public void OnHandleDeleteItem()
    {
        Debug.Log("Deleted it from inventory");
        SaveInventoryManager.data.collectedItems.Remove(uniqueId);
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;

    public ItemPickUpSaveData(int _id, Vector3 _position, Quaternion _rotation)
    {
        id = _id;
        position = _position;
        rotation = _rotation;
    }
}