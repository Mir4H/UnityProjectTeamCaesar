using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToggle : MonoBehaviour, IInteractable
{
    private string _prompt = "Press E to turn the switch";
    public string InteractionPrompt => _prompt;

    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private int RotationAmount = 90;
    [SerializeField] GameObject lever;

    private Vector3 StartRotation;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = lever.transform.rotation.eulerAngles;
    }
    public void TurnSwitch()
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(DoRotation());
    }

    private IEnumerator DoRotation()
    {
        Quaternion startRotation = lever.transform.rotation;
        Quaternion endRotation;

        Debug.Log("start" + startRotation);

        endRotation = Quaternion.Euler(new Vector3(StartRotation.x - RotationAmount, 0, 0));

        float time = 0;
        while (time < 1)
        {
            lever.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime;
        }

        lever.transform.rotation = endRotation;

        StartRotation = lever.transform.rotation.eulerAngles;
    }
    public bool Interact(Player interactor)
    {
        TurnSwitch();

        return true;
    }
}
