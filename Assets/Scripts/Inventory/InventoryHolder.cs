using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected InventorySystem inventorySystem;
    
    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnInventoryDisplay;

    private void Awake()
    {
        inventorySystem = new InventorySystem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.OnOpenInventory();
        }
    }
}

[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem invSystem;
    public Vector3 position;
    public Quaternion rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
    {
        invSystem = _invSystem;
        position = _position;
        rotation = _rotation;
    }
}