using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public static class SaveLoad
{
    public static UnityAction OnSaveInventory;
    public static UnityAction<SaveData> OnLoadInventory;

    private static string directory = "/SaveInventory/";
    private static string fileName = "SaveInventory.sav";

    public static bool Save(SaveData data)
    {
        OnSaveInventory?.Invoke();
        string dir = Application.persistentDataPath + directory;
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dir + fileName, json);

        Debug.Log("saving inventory");
        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveData data = new SaveData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<SaveData>(json);

            OnLoadInventory?.Invoke(data);
        }
        else
        {
            Debug.Log("Save file does not exist");
        }

        return data;
    }
}
