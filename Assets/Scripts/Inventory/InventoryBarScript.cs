using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InventoryBarScript : MonoBehaviour
{
    public GameObject m_slotPrefab;
    [SerializeField] private InventoryHolder inventoryHolder;
    private InventorySystem inventorySystem;

    private void Start()
    {
        gameObject.SetActive(false);
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventoryChanged += OnUpdateInventory;
        }
        else Debug.LogWarning($"No Inventory System assigner to {this.gameObject}");
    }

    private void OnUpdateInventory()
    {
        gameObject.SetActive(true);
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
        Invoke("HideInventory", 2f);
    }

    void HideInventory()
    {
        gameObject.SetActive(false);
    }

    private void DrawInventory()
    {
        foreach(InventoryItem item in inventorySystem.Inventory)
         {
             AddInventorySlot(item);
         }

    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        SlotItemScript slot = obj.GetComponent<SlotItemScript>();
        slot.Set(item);
    }

}
