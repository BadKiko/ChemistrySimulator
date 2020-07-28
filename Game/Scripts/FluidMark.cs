using UnityEngine;
using MyBox;

namespace ChemistrySimulator
{
    public class FluidMark : MonoBehaviour
    {
        // Основа

        [Header("Главный объект к которому присоеденена метка: ")]
        [SerializeField] private GameObject _mainObject;
        [SerializeField] private bool _isThisObject; // Тот ли это объект
                                                     // Если нет то какой это ребенок в родителе


        // С углом
        [Header("Нужен ли угол для метки?")]
        [SerializeField] private bool _needAngle; // Для расчитывания угла куда будет например литься вода, чтобы не тупо вниз уходил луч а расчитыввался угол как при вылевании воды

        [ConditionalField("_needAngle")]
        [SerializeField] private GameObject _helpWithAngleObject; // Пустышка от которой луч идет вниз
        [ConditionalField("_needAngle")]
        [SerializeField] private int _helpAngleObjectChildInt; // Какой по счету объект помогающий определить угол в родителе


        void Show() // Показываем метку
        {



        }

        void Hide()
        {

        }

        //void Old {
        //private void Start()
        //{
        //    objectHelpWithAngle = transform.parent.GetChild(0).GetChild(3).gameObject;
        //}
        //public void ShowMark()
        //{
        //    this.gameObject.SetActive(true); // Скрипт находится на объекте сразу, поэтому не указываю

        //    Vector3 BeginLerp = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z); // Получение всех координ с самой метки для перетаскивания

        //    AngleCalculation();

        //    Vector3 StartRay = new Vector3(objectHelpWithAngle.transform.position.x, objectHelpWithAngle.transform.position.y - 0.3f, objectHelpWithAngle.transform.position.z);
        //    Vector3 FinishRay = Vector3.down;


        //    if (Physics.Raycast(StartRay, FinishRay, out DownRayHit, 10))
        //    {
        //        if (DownRayHit.collider.gameObject.tag != "NotMark")
        //        {
        //            Debug.Log("With Check - " + DownRayHit.collider.gameObject.name);
        //            Vector3 FinishUpLerp = new Vector3(this.gameObject.transform.position.x, DownRayHit.point.y + 0.01f, this.gameObject.transform.position.z);

        //            this.gameObject.transform.position = Vector3.Lerp(BeginLerp, FinishUpLerp, 0.2f);
        //        }
        //    }

        //    Debug.Log("Without Check - " + DownRayHit.collider.gameObject.name);
        //    Debug.DrawRay(StartRay, FinishRay, Color.green);
        //}


        //public void AngleCalculation()
        //{
        //    Vector3 BeginAngleRay = new Vector3(objectHelpWithAngle.transform.position.x, objectHelpWithAngle.transform.position.y, objectHelpWithAngle.transform.position.z);

        //    Vector3 FinishAngleRay = Vector3.down;

        //    Vector3 BeginLerp = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z); // Получение всех координ с самой метки для перетаскивания

        //    if (Physics.Raycast(BeginAngleRay, FinishAngleRay, out LerpHit, 10))
        //    {
        //        this.gameObject.transform.position = Vector3.Lerp(BeginLerp, LerpHit.point, 0.5f);
        //    }


        //    Debug.DrawRay(BeginAngleRay, FinishAngleRay, Color.red);
        //}

        //public void UnshowMark()
        //{
        //    this.gameObject.SetActive(false);
        //}
    }
}