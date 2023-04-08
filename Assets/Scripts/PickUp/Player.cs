using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 2f;
    
    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;
    private Rigidbody heldObjRB;

    [SerializeField]
    private InputActionReference interactionInput;
    
    private RaycastHit hit;

    [SerializeField] private float pickupForce = 150.0f;

    private void Start()
    {
        interactionInput.action.performed += LiftDrop;
    }

    private void LiftDrop(InputAction.CallbackContext obj)
    {
        if (hit.collider != null && inHandItem == null)
        {
            Debug.Log(hit.collider.name);
            PickupObject(hit.collider.gameObject);
        }
        else
        {
            DropObject();
        }
        if (inHandItem != null)
        {
            MoveObject();
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.transform.parent = pickUpParent;
            heldObjRB.drag = 5;
            inHandItem = pickObj;
        }
    }

    void MoveObject()
    {
        heldObjRB.velocity += (pickUpParent.transform.position + heldObjRB.position) * Time.deltaTime;
        if (Vector3.Distance(inHandItem.transform.position, pickUpParent.position) > 0.1f)
        {
            Vector3 moveDirection = (pickUpParent.position - inHandItem.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.transform.parent = null;
        inHandItem = null;
    }

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit,
            hitRange,
            pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }
    }
}