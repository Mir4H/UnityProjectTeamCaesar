using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [SerializeField] private GameObject lamps;
    [SerializeField] private GameObject candle1;
    [SerializeField] private GameObject candle2;
    private void OnEnable()
    {
        EventManager.TorchInHand += LightsOn;
    }

    private void OnDisable()
    {
        EventManager.TorchInHand -= LightsOn;
    }

    private void LightsOn()
    {
        lamps.SetActive(true);
        candle1.SetActive(true);
        candle2.SetActive(true);
    }
}
