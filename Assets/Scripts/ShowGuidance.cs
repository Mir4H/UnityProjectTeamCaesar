using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowGuidance : MonoBehaviour
{
    [SerializeField] private GameObject guidance;
    [SerializeField] private TextMeshProUGUI guidanceText;
    public bool GuidanceDisplayed = false;

    private void Start()
    {
        guidance.SetActive(false);
    }

    public void SetUpGuidance(string guide)
    {
        guidanceText.text = guide;
        guidance.SetActive(true);
        GuidanceDisplayed = true;
    }

    public void CloseGuidance()
    {
        guidance.SetActive(false);
        GuidanceDisplayed = false;
    }

}
