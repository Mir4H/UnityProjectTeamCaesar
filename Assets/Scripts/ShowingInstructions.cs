using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class ShowingInstructions : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject instructionText;
    [SerializeField] private GameObject instructionText2;
    [SerializeField] private GameObject isCompleteText;

    public static event UnityAction showInstructions;
    public static event UnityAction compeleted;
    public static void OnShowInstructions() => showInstructions?.Invoke();
    public static void OnShowCompeleted() => compeleted?.Invoke();

    private void OnEnable()
    {
        showInstructions += InstructionsRequested;
        compeleted += TaskComplete;
    }
    private void TaskComplete()
    {
        if (isCompleteText != null)
        {
            instructions.SetActive(true);
            instructionText.SetActive(false);
            if (instructionText2 != null) instructionText2.SetActive(false);
            isCompleteText.SetActive(true);
            Invoke("Close", 13f);
        }
    }

    private void InstructionsRequested()
    {
        instructions.SetActive(true);
        instructionText.SetActive(true);
        if (instructionText2 != null) instructionText2.SetActive(false);
        if (isCompleteText != null) isCompleteText.SetActive(false);
        if (instructionText2 != null) Invoke("OpenSecond", 18f);
        else Invoke("Close", 20f);
    }

    private void OpenSecond()
    {
        instructions.SetActive(true);
        instructionText.SetActive(false);
        instructionText2.SetActive(true);
        if (isCompleteText != null) isCompleteText.SetActive(false);
        Invoke("Close", 15f);
    }

    private void Close()
    {
        instructions.SetActive(false);
    }

    private void OnDisable()
    {
        showInstructions -= InstructionsRequested;
        compeleted -= TaskComplete;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if (instructions.activeSelf)
            {
                Close();
                CancelInvoke();
            }
            else
            {
                InstructionsRequested();
            }
        }
    }
}
