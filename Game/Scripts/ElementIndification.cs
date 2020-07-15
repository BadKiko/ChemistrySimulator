using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementIndification : MonoBehaviour
{
    [Header("Что это за эелемент?:")]
    public int ElementIs = 0;
    [Header("Копия материала для его изменения:")]
    [SerializeField] private Renderer ElementRenderer; // Беру рендер элемента для копии материала
    [Header("Взаимодействует ли с воздухом?:")]
    public bool InteractWithOxygen;
    [Header("Промежутки цветов при окислении:")]
    [SerializeField] Color ColorWithoutOxygen, ColorWithOxygen;
    [Header("Скорость окисления:")]
    [SerializeField] private float OxygenSpeed;



    //[Header("В жидкости ли металл?")]
    //[SerializeField] private bool MetallInFluid;
    private void Start()
    {
        if(ElementIs == 2)
        {
            Lithium();
        }
    }

    public void Lithium()
    {
        ////////////////////////////////
        
        ElementRenderer = this.gameObject.GetComponent<Renderer>();
        ElementRenderer.material = this.gameObject.GetComponent<Renderer>().material;
        this.gameObject.GetComponent<Renderer>().material = ElementRenderer.material; // Если не сделать этого то все кусочки будут менять цвет, тк материал один на всех, а таким образом мы их разбиваем.

        ////////////////////////////////

        // Дальше проверка на воздух в апдейте смотреть InteractWithOxygen
    }

    private void LithiumInFluid()
    {

    }

    private void Update()
    {
        if (InteractWithOxygen) // Для лерпа нужен update
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(ColorWithoutOxygen, ColorWithOxygen, OxygenSpeed);
            ColorWithoutOxygen = this.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Lithium - Oxygen");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "CanAddLiquid") // Если камень падает в жидкость
        {
            LithiumInFluid();
        }
    }
}
