using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        float clipLength = isCompleteText.GetComponent<AudioSource>().clip.length;

        if (isCompleteText != null)
        {
            instructions.SetActive(true);
            instructionText.SetActive(false);
            if (instructionText2 != null) instructionText2.SetActive(false);
            isCompleteText.SetActive(true);
            Invoke("Close", clipLength + 1);
        }
    }

    private void InstructionsRequested()
    {
        float clipLength = instructionText.GetComponent<AudioSource>().clip.length;
        instructions.SetActive(true);
        instructionText.SetActive(true);
        if (instructionText2 != null) instructionText2.SetActive(false);
        if (isCompleteText != null) isCompleteText.SetActive(false);
        if (instructionText2 != null) Invoke("OpenSecond", clipLength + 1);
        else Invoke("Close", clipLength + 1);
    }

    private void OpenSecond()
    {
        float clipLength = instructionText2.GetComponent<AudioSource>().clip.length;
        instructions.SetActive(true);
        instructionText.SetActive(false);
        instructionText2.SetActive(true);
        if (isCompleteText != null) isCompleteText.SetActive(false);
        Invoke("Close", clipLength + 1);
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
