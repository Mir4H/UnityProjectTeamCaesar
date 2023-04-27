using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToggle : MonoBehaviour, IInteractable
{
    private string _prompt = "Press E to turn the switch";
    public string InteractionPrompt => _prompt;

    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private int RotationAmount = 90;

    private Vector3 StartRotation;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.localRotation.eulerAngles;
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
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation;

        Debug.Log("start" + startRotation);

        if (StartRotation.x <= 46f)
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x - RotationAmount, 0, 0));

            float time = 0;
            while (time < 1)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime;
            }
            transform.localRotation = endRotation;
        }

        if (StartRotation.x >= 315f)
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x + RotationAmount, 0, 0));

            float time = 0;
            while (time < 1)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime;
            }

            transform.localRotation = endRotation;
        }

        StartRotation = transform.localRotation.eulerAngles;
        Debug.Log("Start Rotation: " + StartRotation);
    }
    public bool Interact(Player interactor)
    {
        TurnSwitch();

        return true;
    }
}
