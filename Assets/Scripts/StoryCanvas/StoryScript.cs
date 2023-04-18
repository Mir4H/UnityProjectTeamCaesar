using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    public InventorySystem inventory;
    [SerializeField] Button next;
    [SerializeField] Button prev;
    [SerializeField] GameObject partOne;
    [SerializeField] GameObject partTwo;
    [SerializeField] GameObject partThree;
    [SerializeField] GameObject partFour;
    private Button selectedItem;
    private int nroOfScrolls;
    public static int currentlyShown = 0;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (var item in inventory.Container.Items)
        {
            if(item.Item.Name == "Scroll")
            {
                nroOfScrolls = item.StackSize;
                currentlyShown = nroOfScrolls;
                Debug.Log(currentlyShown);
            }
        }

        if (nroOfScrolls > 1)
        {
            next.gameObject.SetActive(true);
            prev.gameObject.SetActive(true);

        }
        selectedItem = gameObject.transform.Find("Exit").gameObject.GetComponentInChildren<Button>();
        selectedItem.Select();
        selectedItem.OnSelect(null);
    }

    private void Update()
    {
        //Debug.Log(currentlyShown);
        if (currentlyShown > nroOfScrolls) currentlyShown = 1;
        if (currentlyShown < 1) currentlyShown = nroOfScrolls;
        if (currentlyShown == 1) partOne.gameObject.SetActive(true); else partOne.gameObject.SetActive(false);
        if (currentlyShown == 2) partTwo.gameObject.SetActive(true); else partTwo.gameObject.SetActive(false);
        if (currentlyShown == 3) partThree.gameObject.SetActive(true); else partThree.gameObject.SetActive(false);
        if (currentlyShown == 4) partFour.gameObject.SetActive(true); else partFour.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        partOne.gameObject.SetActive(false);
        partTwo.gameObject.SetActive(false);
        partThree.gameObject.SetActive(false);
        partFour.gameObject.SetActive(false);
    }

}
