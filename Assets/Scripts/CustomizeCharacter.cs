using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] shirt;
    [SerializeField] private GameObject[] pants;
    [SerializeField] private GameObject[] skin;
    [SerializeField] private GameObject[] mustaches;
    [SerializeField] private GameObject[] beard;

    private int currentShirt;
    private int currentPants;
    private int currentSkin;
    private int currentMustaches;
    private int currentBeard;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < shirt.Length; i++)
        {
            if (i == currentShirt)
            {
                shirt[i].SetActive(true);
            } else
            {
                shirt[i].SetActive(false);
            }
        }

        for (int i = 0; i < mustaches.Length; i++)
        {
            if (i == currentMustaches)
            {
                mustaches[i].SetActive(true);
            }
            else
            {
                mustaches[i].SetActive(false);
            }
        }

        for (int i = 0; i < beard.Length; i++)
        {
            if (i == currentBeard)
            {
                beard[i].SetActive(true);
            }
            else
            {
                beard[i].SetActive(false);
            }
        }
    }

    public void SwitchShirt()
    {
        if (currentShirt == shirt.Length - 1)
        {
            currentShirt = 0;
        }
        else
        {
            currentShirt++;
        }

    }

    public void SwitchMustaches()
    {
        if (currentMustaches == mustaches.Length - 1)
        {
            currentMustaches = 0;
        }
        else
        {
            currentMustaches++;
        }

    }

    public void SwitchBeard()
    {
        if (currentBeard == beard.Length - 1)
        {
            currentBeard = 0;
        }
        else
        {
            currentBeard++;
        }

    }
}
