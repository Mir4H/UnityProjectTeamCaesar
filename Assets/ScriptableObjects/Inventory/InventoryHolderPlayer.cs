using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryHolderPlayer : MonoBehaviour
{
    public InventorySystem inventory;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] InputActionReference interactionInput;
    private GameObject collectableItem;

    private bool useLookAt;
    private Vector3 _targetPosition;

    private void Awake()
    {
        SaveLoad.OnLoadInventory += LoadInventory;
    }


    private void LoadInventory(SaveData data)
    {
        var keysToDelete = new List<string>();
        foreach (KeyValuePair<string, ItemPickUpSaveData> entry in data.activeItems)
        {
            var exists = FindObjectOfType<UniqueID>().ID == entry.Key;
            if (!exists)
            {
                Instantiate(inventory.database.GetItem[entry.Value.id].prefab, entry.Value.position, entry.Value.rotation);
                keysToDelete.Add(entry.Key);
            }
        }
        foreach (string id in keysToDelete)
        {
            if (SaveInventoryManager.data.activeItems.ContainsKey(id)) SaveInventoryManager.data.activeItems.Remove(id);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PointOfInterest")
        {
            Debug.Log("Seeing point of interest");
            pickUpUI.SetActive(true);
            _targetPosition = other.transform.position;
            useLookAt = true;
            collectableItem = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        useLookAt = false;
        collectableItem = null;
    }

    private void Collect(InputAction.CallbackContext obj)
    {
        if (useLookAt && collectableItem != null)
        {
            var item = collectableItem.GetComponent<ItemCollectable>();
            if (item)
            {
                inventory.AddItem(new Item(item.item));
                collectableItem.GetComponent<ItemObject>().OnHandlePickupItem();
            }
            return;
        }
        return;
    }

    private void Update()
    {
        if (!useLookAt || !collectableItem)
        {
            pickUpUI.SetActive(false);
            _targetPosition = _parent.position + _parent.forward * 2f + new Vector3(0f, 2f, 0f);
        }
        _lookAtTarget.transform.position = Vector3.Lerp(_lookAtTarget.transform.position, _targetPosition, Time.deltaTime * lookSpeed);
        interactionInput.action.performed += Collect;

        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.OnOpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.Load();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
            inventory.Container.Items.Clear();
    }


         
}
