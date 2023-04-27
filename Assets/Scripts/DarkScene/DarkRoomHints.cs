using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoomHints : MonoBehaviour
{
    [SerializeField] private ShowGuidance showGuidance;
    [SerializeField] private GameObject lamps;
    [SerializeField] private GameObject middleDoor;
    [SerializeField] private GameObject goalDoor;

    private void OnEnable()
    {
        EventManager.FirstPartSolved += ThroneHints;
        EventManager.SecondPartSolved += MathHints;
    }

    private void Start()
    {
        Invoke("FirstHint", 30f);
    }

    private void FirstHint()
    {
        if (!lamps.activeInHierarchy)
        {
            showGuidance.SetUpGuidance("Maybe check your inventory for help?");
            Invoke("Close", 10f);
            Invoke("SecondHint", 30f);
        }
    }

    private void SecondHint()
    {
        if (!lamps.activeInHierarchy)
        {
            showGuidance.SetUpGuidance("Open inventory with I and try getting torch.");
            Invoke("Close", 10f);
        }
    }

    private void ThroneHints()
    {
        Invoke("ThirdHint", 40f);
    }
    private void ThirdHint()
    {
        if (middleDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("Is there some picture to help with thrones?");
            Invoke("Close", 10f);
            Invoke("FourthHint", 40f);
        }
    }

    private void FourthHint()
    {
        if (middleDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("The arrows on the wall show how to turn thrones. Down means facing you.");
            Invoke("Close", 10f);
        }
    }

    private void MathHints()
    {
        Invoke("FifthHint", 60f);
    }

    private void FifthHint()
    {
        if (goalDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("The colors in the numbers might have a meaning.");
            Invoke("Close", 10f);
            Invoke("SixthHint", 60f);
        }
    }

    private void SixthHint()
    {
        if (goalDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("Do the calculations in the first room and change the same colored number to match the answer.");
            Invoke("Close", 10f);
            Invoke("SixthHint", 60f);
        }
    }

    private void Close()
    {
        showGuidance.CloseGuidance();
    }

    private void OnDisable()
    {
        EventManager.FirstPartSolved -= ThroneHints;
        EventManager.SecondPartSolved -= MathHints;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (!lamps.activeInHierarchy)
        {
            showGuidance.SetUpGuidance("Maybe check your inventory for help?");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        showGuidance.CloseGuidance();
    }*/
}
