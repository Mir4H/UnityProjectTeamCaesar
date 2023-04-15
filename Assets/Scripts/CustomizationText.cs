using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomizationText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] Customizable _customizable;

    private void OnValidate()
    {
        _text = GetComponent<TMP_Text>();
        _customizable = FindObjectOfType<Customizable>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText(_customizable.CurrentCustomization?.DisplayName);
    }
}
