using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasInventory : MonoBehaviour
{
    public GameObject inventoryBar;
    private Button selectedItem;

    private void OnEnable()
    {
        EventManager.OpenInventory += EventManagerOnOpenInventory;
    }

    private void EventManagerOnOpenInventory()
    {
        if (inventoryBar.activeSelf)
        {
            inventoryBar.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("i hear u");
            inventoryBar.SetActive(true);
            Time.timeScale = 0;
            selectedItem = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Button>();
            selectedItem.Select();
            selectedItem.OnSelect(null);
        }
    }

    private void OnDisable()
    {
        EventManager.OpenInventory -= EventManagerOnOpenInventory;
    }
}