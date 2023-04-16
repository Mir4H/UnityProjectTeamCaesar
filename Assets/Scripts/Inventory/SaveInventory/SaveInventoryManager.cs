using UnityEngine;
using UnityEngine.Events;

public class SaveInventoryManager : MonoBehaviour
{
    public static SaveData data;
    public static UnityAction OnSaveInventoryItems;
    public static UnityAction OnLoadInventoryItems;
    public static UnityAction OnClearInventoryItems;

    private void Awake()
    {
        data = new SaveData();
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadInventory += LoadData;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
        OnClearInventoryItems?.Invoke();
    }

    public static void SaveData()
    {
        var saveData = data;
        SaveLoad.Save(saveData);
        OnSaveInventoryItems?.Invoke();
    }

    private static void LoadData(SaveData _data)
    {
        data = _data;
        OnLoadInventoryItems?.Invoke();
    }

    public static void TryLoadData()
    {
        SaveLoad.Load();
    }

    private void OnDisable()
    {
        SaveLoad.OnLoadInventory -= LoadData;
    }
}
