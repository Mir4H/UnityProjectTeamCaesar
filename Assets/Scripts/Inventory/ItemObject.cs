using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickupItem(InventoryHolder inventory)
    {
        Debug.Log("I collected it");
        inventory.InventorySystem.Add(referenceItem);
        //InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }

    public void OnHandleDeleteItem(InventoryHolder inventory)
    {
        Debug.Log("Deleted it fro inventory");
        inventory.InventorySystem.Remove(referenceItem);
    }
}
