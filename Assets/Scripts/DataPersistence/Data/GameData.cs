using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        // Here should be the player starting point value: -5.7, 0.2500001, -9.93
        this.playerPosition = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
    }
}
