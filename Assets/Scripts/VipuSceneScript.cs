using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipuSceneScript : MonoBehaviour
{
    [SerializeField] private GameObject switch1;
    [SerializeField] private GameObject switch2;
    [SerializeField] private GameObject switch3;
    [SerializeField] private GameObject switch4;
    [SerializeField] private GameObject switch5;
    [SerializeField] private GameObject switch6;

    [SerializeField] private GameObject fence1;
    [SerializeField] private GameObject fence2;
    [SerializeField] private GameObject fence3;
    [SerializeField] private GameObject fence4;

    [SerializeField] private GameObject goalDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCheck();
    }

    // Up: 315, Down: 45
    private void SwitchCheck()
    {
        if (switch1.transform.eulerAngles.x == 315 &&
            switch2.transform.eulerAngles.x == 45 &&
            switch3.transform.eulerAngles.x == 315 &&  
            switch4.transform.eulerAngles.x == 315 &&
            switch5.transform.eulerAngles.x == 45 &&
            switch6.transform.eulerAngles.x == 45)
        {
            Debug.Log("thrones right");

            GameObject.Destroy(fence1);
            GameObject.Destroy(fence2);
            GameObject.Destroy(fence3);
            GameObject.Destroy(fence4);
        } else
        {
            Debug.Log("thrones wrong");
            Debug.Log(switch1.transform.eulerAngles.x);
            Debug.Log(switch2.transform.eulerAngles.x);
        }
    }
}
