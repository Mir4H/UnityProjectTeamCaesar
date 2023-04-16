using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "InventorySystem/Inventory")]
public class InventorySystem :  ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public InventoryUsed Container;
    public UnityAction<bool> OnInventoryChanged;

    private void OnEnable()
    {
        EventManager.LoadInventoryItems += Load;
        EventManager.SaveInventoryItems += Save;
        SaveInventoryManager.OnClearInventoryItems += Clear;
    }
    private void OnDisable()
    {
        EventManager.LoadInventoryItems -= Load;
        EventManager.SaveInventoryItems -= Save;
        SaveInventoryManager.OnClearInventoryItems -= Clear;
    }

    public void AddItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].Item.Id == _item.Id)
            {
                Container.Items[i].AddToStack();
                OnInventoryChanged?.Invoke(true);
                return;
            }
        }
        Container.Items.Add(new InventoryItem(_item.Id, _item));
        OnInventoryChanged?.Invoke(true);
    }

    public void RemoveItem(ItemCollectable _item)
    {
        var itemToRemove = Container.Items.Find(item => item.ID == _item.item.Id);

        if(itemToRemove.StackSize > 1)
        {
            itemToRemove.RemoveFromStack();
        }
        else
        {
            Container.Items.Remove(itemToRemove);
        }
        OnInventoryChanged?.Invoke(false);
    }

    [ContextMenu("Save")]
    public void Save(string selectedProfileId)
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        string fullPath = Path.Combine(Application.persistentDataPath, selectedProfileId, savePath);
        FileStream file = File.Create(fullPath);
        //GUIUtility.systemCopyBuffer = string.Concat(fullPath);
        bf.Serialize(file, saveData);
        file.Close();

        /*
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();*/
    }
    [ContextMenu("Load")]
    public void Load(string selectedProfileId)
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            string fullPath = Path.Combine(Application.persistentDataPath, selectedProfileId, savePath);
            FileStream file = File.Open(fullPath, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            OnInventoryChanged?.Invoke(false);

           /* IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (InventoryUsed)formatter.Deserialize(stream);
            stream.Close();*/
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new InventoryUsed();
    }
}

[System.Serializable]
public class InventoryUsed
{
    public List<InventoryItem> Items = new List<InventoryItem>();
}

[System.Serializable]
public class InventoryItem
{
    public int ID;
    public Item Item;
    public int StackSize;

    public InventoryItem(int _id, Item source)
    {
        ID = _id;
        Item = source;
        AddToStack();
    }

    public void AddToStack()
    {
        StackSize++;
    }

    public void RemoveFromStack()
    {
        StackSize--;
    }
}