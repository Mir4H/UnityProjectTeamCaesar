using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour  
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("backwall"))
        {
            EventManager.OnBoxStucked();
            Destroy(gameObject, 5f); 
        }
    }
}
