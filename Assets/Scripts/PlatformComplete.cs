using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformComplete : MonoBehaviour
{
    [SerializeField] private GameObject goldenKey;
    [SerializeField] private GameObject goalDoor;
    private void OnEnable()
    {
        goldenKey.SetActive(true);
        goalDoor.tag = "goal";
        ShowingInstructions.OnShowCompeleted();

    }

    private void OnDisable()
    {
        goldenKey.SetActive(false);
    }
}
