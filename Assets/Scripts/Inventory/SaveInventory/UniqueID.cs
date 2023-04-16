using System;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private static Serializable<string, GameObject> idDatabase = new Serializable<string, GameObject>();

    public string ID => _id;

    private void OnValidate()
    {
        if (idDatabase.ContainsKey(_id)) Generate();
        else idDatabase.Add(_id, this.gameObject);
    }

    private void OnDestroy()
    {
        if (idDatabase.ContainsKey(_id)) idDatabase.Remove(_id);
    }

    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        idDatabase.Add(_id, this.gameObject);
    }
}
