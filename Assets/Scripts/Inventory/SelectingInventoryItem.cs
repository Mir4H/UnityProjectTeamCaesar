using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;

public class SelectingInventoryItem : MonoBehaviour
{ 
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        var allKids = GetComponentsInChildren<Transform>();
        var kid = allKids.Where(k => k.gameObject.name == "Label").FirstOrDefault();
        Debug.Log(kid.GetComponent<TextMeshProUGUI>().text);
        EventManager.OnGetInventoryItem(kid.GetComponent<TextMeshProUGUI>().text);
        EventManager.OnCloseInventory();

    }

}
