using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public GameObject shirt;
    public Material[] shirtMaterials;

    public GameObject pants;
    public Material[] pantsMaterials;

    public List<GameObject> bodyParts;
    public Material[] skinMaterials;

    public GameObject[] moustaches;
    private int currentMoustache;

    public GameObject[] beard;
    private int currentBeard;

    private void Update()
    {
        for (int j = 0; j < beard.Length; j++)
        {
            if (j == currentBeard)
            {
                beard[j].SetActive(true);
            }
            else
            {
                beard[j].SetActive(false);
            }
        }

        for (int i = 0; i < moustaches.Length; i++)
        {
            if (i == currentMoustache)
            {
                moustaches[i].SetActive(true);
            }
            else
            {
                moustaches[i].SetActive(false);
            }
        }
    }

    public void changeShirt(int index)
    {
        if (index >= shirtMaterials.Length) return;
        shirt.GetComponent<Renderer>().material = shirtMaterials[index];
    }

    public void changePants(int index)
    {
        if (index >= pantsMaterials.Length) return;
        pants.GetComponent<Renderer>().material = pantsMaterials[index];
    }

    public void changeSkin(int index)
    {
        if (index >= skinMaterials.Length) return;


        foreach (var bodyPart in bodyParts)
        {
            bodyPart.GetComponent<Renderer>().material = skinMaterials[index];
        }
    }

    public void SetMoustaches()
    {
        if (currentMoustache == moustaches.Length - 1)
        {
            currentMoustache = 0;
        }
        else
        {
            currentMoustache++;
        }

    }

    public void SetBeard()
    {
        if (currentBeard == beard.Length - 1)
        {
            currentBeard = 0;
        } else
        {
            currentBeard++;
        }

    }

}

   
