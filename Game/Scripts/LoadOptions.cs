using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOptions : MonoBehaviour
{

    private int ModelsQuality, ReflectionsQuality, LightQuality;
    bool WS, SokM, Lokm;
    [SerializeField] private GameObject[] ModellsQualityObjects, ReflectionQualityObjects;
    void Start()
    {
        ModelsQuality = PlayerPrefs.GetInt("ModelsQuality");
        ReflectionsQuality = PlayerPrefs.GetInt("ReflectionsQuality");

        ModelsQualityLoad();
        //ReflectionsQualityVoid();
    }
    void ModelsQualityLoad()
    {

        if (ModelsQuality == 0)
        {
            Application.LoadLevel(3);
        }
        if (ModelsQuality == 1)
        {
            Application.LoadLevel(4);
        }

        if (ModelsQuality == 2)
        {
            Application.LoadLevel(5);
        }

        if (ModelsQuality == 3)
        {
            Application.LoadLevel(6);
        }

        if (ModelsQuality == 4)
        {
            Application.LoadLevel(7);
        }

    }
//    void ReflectionsQualityVoid()
//{
//    if (ReflectionsQuality == 0) {
//        ReflectionQualityObjects[0].SetActive(true);
//        Debug.Log("MQ2");
//        for (int elements = 1; elements <= ReflectionQualityObjects.Length; elements++)
//        {
//            ReflectionQualityObjects[elements].SetActive(false);
//        }
//    }


//    if (ReflectionsQuality == 1)
//    {
//        ReflectionQualityObjects[1].SetActive(true);
//        for (int elements = 2; elements <= ReflectionQualityObjects.Length; elements++)
//        {
//            ReflectionQualityObjects[elements].SetActive(false);
//        }

//    }

//    if (ReflectionsQuality == 2)
//    {
//        ReflectionQualityObjects[2].SetActive(true);
//        for (int elements = 3; elements <= ReflectionQualityObjects.Length; elements++)
//        {
//            ReflectionQualityObjects[elements].SetActive(false);
//        }
//    }


//    if (ReflectionsQuality == 3)
//    {
//        ReflectionQualityObjects[3].SetActive(true);
//        for (int elements = 4; elements <= ReflectionQualityObjects.Length; elements++)
//        {
//            ReflectionQualityObjects[elements].SetActive(false);
//        }
//    }


//}
}
