using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialController : MonoBehaviour, IDataPersistence
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

    private bool isBeard = false;
    private bool isMoustache = false;
    private Material shirtChoise;
    private Material pantsChoise;
    private Material skinChoise;

    public void LoadData(GameData data)
    {
        //
    }

    public void SaveData(GameData data)
    {
        data.beard = isBeard;
        data.moustache = isMoustache;
        data.shirt = shirtChoise;
        data.pants = pantsChoise;
        data.skin = skinChoise;
    }


    private void Update()
    {
        for (int j = 0; j < beard.Length; j++)
        {
            if (j == currentBeard)
            {
                beard[j].SetActive(true);
                isBeard = false;
            }
            else
            {
                beard[j].SetActive(false);
                isBeard = true;
            }
        }

        for (int i = 0; i < moustaches.Length; i++)
        {
            if (i == currentMoustache)
            {
                moustaches[i].SetActive(true);
                isMoustache = false;
            }
            else
            {
                moustaches[i].SetActive(false);
                isMoustache = true;
            }
        }
    }

    public void changeShirt(int index)
    {
        if (index >= shirtMaterials.Length) return;
        shirt.GetComponent<Renderer>().material = shirtMaterials[index];
        shirtChoise = shirtMaterials[index];
    }

    public void changePants(int index)
    {
        if (index >= pantsMaterials.Length) return;
        pants.GetComponent<Renderer>().material = pantsMaterials[index];
        pantsChoise = pantsMaterials[index];
    }

    public void changeSkin(int index)
    {
        if (index >= skinMaterials.Length) return;


        foreach (var bodyPart in bodyParts)
        {
            bodyPart.GetComponent<Renderer>().material = skinMaterials[index];
            skinChoise = skinMaterials[index];
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

   
