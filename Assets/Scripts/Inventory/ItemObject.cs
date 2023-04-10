using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickupItem()
    {
        Debug.Log("I collected it");
        InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }
}
