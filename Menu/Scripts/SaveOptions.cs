
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour
{

    [SerializeField] private Dropdown ModelsQuality, ReflectionsQuality;
    [SerializeField] private Toggle MotionBlur, SmokeInPause;
    [SerializeField] private Button ApplyOptions;

    private void Start()
    {
        ApplyOptions.onClick.AddListener(SaveOptionsVoid);
    }

    private void SaveOptionsVoid()
    {
        PlayerPrefs.SetInt("ModelsQuality", ModelsQuality.value);
        Debug.Log("ModelsQuality - " + PlayerPrefs.GetInt("ModelsQuality"));
        PlayerPrefs.SetInt("ReflectionsQuality", ReflectionsQuality.value);
        Debug.Log("ReflectionsQuality - " + PlayerPrefs.GetInt("ReflectionsQuality"));
        PlayerPrefs.SetInt("MotionBlur", Convert.ToInt32(MotionBlur.isOn)); // конвертим bool в int 
        Debug.Log("MotionBlur - " + PlayerPrefs.GetInt("MotionBlur"));
        PlayerPrefs.SetInt("SmokeInPause", Convert.ToInt32(SmokeInPause.isOn)); // конвертим bool в int 
        Debug.Log("SmokeInPause - " + PlayerPrefs.GetInt("SmokeInPause"));

    }
}
