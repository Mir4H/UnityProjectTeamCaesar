using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCondition : MonoBehaviour
{
    [SerializeField] private GameObject bowlOrange;
    [SerializeField] private GameObject bowlPurple;
    [SerializeField] private GameObject bowlGreen;
    [SerializeField] private GameObject potionBowl;
    [SerializeField] private GameObject key;

    private void Awake()
    {
        potionBowl.SetActive(false);
        key.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bowlGreen.gameObject.tag == "GreenBag" && bowlOrange.gameObject.tag == "OrangeBag" && bowlPurple.gameObject.tag == "PurpleBag")
        {
            Debug.Log("done");
            potionBowl.SetActive(true);
            key.SetActive(true);
            Destroy(gameObject);
        }
            
    }
}
