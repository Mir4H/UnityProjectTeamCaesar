using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecryptCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button submitBtn;
    [SerializeField] private Button QuitBtn;
    [SerializeField] private GameObject resolve;
    [SerializeField] private TextMeshProUGUI message;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        submitBtn.onClick.AddListener(GetInputOnClick);
        QuitBtn.onClick.AddListener(Close);
    }

    private void Close()
    {
        resolve.SetActive(false);
    }

    public void GetInputOnClick()
    {
        Debug.Log(input.text);
        if (input.text.ToLower() == "caesar")
        {
            Debug.Log("read diary");
            Close();
        }
        else
        {
            message.text = $"{input.text} is incorrect. Please try again or try to look for clues!";
            //Close();           
        }
    }

}
