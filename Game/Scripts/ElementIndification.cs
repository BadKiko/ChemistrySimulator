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
    [Header("Окисление:")]
    [Header("Какой эффект пременить к объекту? :")]
    [SerializeField] private bool Oxidation = false;


    [Header("Объект с которым сталкивается наш объект:")]
    [SerializeField] private GameObject CollisionObject;

    [Header("Эффект который добавляется при окислении: ")]
    [SerializeField] private GameObject BubbleGameObject;


    private Vector3 BeginLerp;
    private Vector3 FinishLerp;
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
        Oxidation = true; // Ставим эффект окисления при столкновении с водой

        this.gameObject.transform.localRotation.eulerAngles.Set(0, 0, 0); // Ставим zero чтобы все камни лерпались по одинаковому

        Instantiate(BubbleGameObject, this.gameObject.transform); // Создаеем пузырение а затем ставим на false чтобы не создавать много.

        this.gameObject.AddComponent<StoneInLiquid>(); // Добовляем нашему камню при столкновении скрипт связанный с водой.
                                                       // Он будет контроллировать высоту камню чтобы камень плавал на поверхности воды.

        this.gameObject.GetComponent<StoneInLiquid>().Stone = this.gameObject; // Даем скрипту наш камень чтобы он менял его положение

        this.gameObject.GetComponent<StoneInLiquid>().LiquidRenderer = CollisionObject.GetComponent<Renderer>(); // Получаем материал чтобы брать FluidAmmount из шеййдера

        this.gameObject.transform.SetParent(CollisionObject.transform); // Ставим родительским объектом у камня жидкость чтобы сбросить координату на 0.

        this.gameObject.GetComponent<BoxCollider>().enabled = false; // Выключаем коллицзию у камня чтобы он мог спокойно быть в колбе и не вылетать из нее
        Destroy(this.gameObject.GetComponent<Rigidbody>()); // Уничтожаем Rigidbody тк нам не нужна физика чтобы он падал вниз

        this.gameObject.transform.localPosition = Vector3.zero; // Ставим локальную позицию по 0
    }

    private void Update()
    {
        if (InteractWithOxygen) // Для лерпа нужен update
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(ColorWithoutOxygen, ColorWithOxygen, OxygenSpeed);
            ColorWithoutOxygen = this.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Lithium - Oxygen");
        }


        // Effects

        if(Oxidation == true)
        {
            BeginLerp = this.gameObject.transform.localPosition;
            FinishLerp = new Vector3(Random.Range(-0.005f, 0.005f), Random.Range(-0.005f, 0.005f), 0);


            this.gameObject.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.03f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "CanAddLiquid") // Если камень падает в жидкость
        {
            CollisionObject = collision.collider.gameObject; // Присваеваем объект с которым столкнулся наш камень другому чтобы в дальнейшем это использовать

            LithiumInFluid(); // Запускаем метод когда литий падает в воду
        }
    }
}
