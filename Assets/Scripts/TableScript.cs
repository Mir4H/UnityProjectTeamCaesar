using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

public class TableScript : MonoBehaviour
{
    [SerializeField] private GameObject goalDoor;
    private int carrots = 0;
    private int bread = 0;
    private int mug = 0;
    private int meat = 0;


    private void OnTriggerEnter(Collider other)
    {
       
        if ( other.gameObject.tag != "Player")
        {
            if (other.gameObject.name.Contains("Carrot")) carrots += 1;
            if (other.gameObject.name.Contains("Bread")) bread += 1;
            if (other.gameObject.name.Contains("Mug")) mug += 1;
            if (other.gameObject.name.Contains("Meat")) meat += 1;
            other.gameObject.tag = "Untagged";
            other.gameObject.name = "TableItem";
            Debug.Log("carrots " + carrots);
        }

        if (carrots >= 2 && bread >= 2 && mug >= 1 && meat >= 1)
        {
            goalDoor.gameObject.tag = "goal";
            ShowingInstructions.OnShowCompeleted();
            Debug.Log("done");
        }
    }
}
