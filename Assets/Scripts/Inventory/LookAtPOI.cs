using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class LookAtPOI : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] InputActionReference interactionInput;
    private GameObject collectableItem;

    private bool useLookAt;
    private Vector3 _targetPosition;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PointOfInterest")
        {
            Debug.Log("Seeing point of interest");
            pickUpUI.SetActive(true);
            _targetPosition = collision.transform.position;
            useLookAt = true;
            collectableItem = collision.gameObject;
            
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        useLookAt = false;
        collectableItem = null;
    }

    private void Update()
    {
        if (!useLookAt || !collectableItem)
        {
            pickUpUI.SetActive(false);
            _targetPosition = _parent.position + _parent.forward * 2f + new Vector3(0f,2f,0f);
        }
        _lookAtTarget.transform.position = Vector3.Lerp(_lookAtTarget.transform.position, _targetPosition, Time.deltaTime * lookSpeed);
        interactionInput.action.performed += Collect;
    }

    private void Collect(InputAction.CallbackContext obj)
    {
        if(useLookAt && collectableItem != null)
        {
            Debug.Log(collectableItem.ToString());
            collectableItem.TryGetComponent<ItemObject>(out ItemObject item);
            item.OnHandlePickupItem(GetComponent<InventoryHolder>());
        }
        return;
    }
}
