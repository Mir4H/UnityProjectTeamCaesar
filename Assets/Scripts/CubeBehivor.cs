using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeBehivor : MonoBehaviour
{
    public Transform startPosition;
    public Rigidbody box;
    public GameObject boxy;


    void Start()
    {
        Instantiate(box, startPosition.position, Quaternion.identity);
    }


    private void OnEnable()
    {
        EventManager.NewBox += EventManagerOnBoxStucked;
    }

    private void EventManagerOnBoxStucked()
    {
        Rigidbody newbox;
        newbox = Instantiate(box, startPosition.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        EventManager.NewBox -= EventManagerOnBoxStucked;
    }


    void Update()
    {/*
        if (box.transform.position.x >= 16.7)
        {
            Rigidbody newbox;
            newbox = Instantiate(box, startPosition.position, Quaternion.identity);
        }*/
    }
/*
    void NewObject()
    {
        Instantiate(box, startPosition.position, Quaternion.identity);
    }*/
}

