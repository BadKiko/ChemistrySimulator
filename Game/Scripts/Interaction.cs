using UnityEngine;
using MyBox; // Атрибут для того чтобы скрывать другие атрибуты при bool

namespace ChemistrySimulator
{

    public class Interaction : MonoBehaviour
    {
        // Основное
        private GameObject _rayHitGameObject; // Объект в который попадает наш луч
        [Header("Камера игрока:")]
        [SerializeField] private Camera _mainCamera; // Главная камера которая стоит на игроке

        // Лучи
        private Vector3 _beginRay; // Начало луча
        private Vector3 _finishRay; // Конец луча

        RaycastHit _objectHit; // Куда попал луч

        // Тэги
        private string _objectTag; // Тэг с объекта на который попал луч
        [Header("Тэги которые участвуют в взаимодействиях")]
        [SerializeField] private string[] _tagList; // Тэги которые участвуют в взаимодействиях


        private void Update()
        {
            _beginRay = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2)); // Получаем origin луча с помощью деления экрана
            _finishRay = _mainCamera.transform.TransformDirection(Vector3.forward); // Конец луча TransformDirection нужен чтобы луч всегда смотрел вперед не зваися от позиции объекта

            TakeRaycastHit(); // Вызываем рэйкаст

            Debug.DrawRay(_beginRay, _finishRay * 6f, Color.red); // Линия куда 
        }
        private void TakeRaycastHit() // Смотрим куда попал наш луч
        {
            if (Physics.Raycast(_beginRay, _finishRay, out _objectHit, 4))
            {
                if(_objectHit.collider != null) // Смотрим чтобы наш коллайдер не был равен нулю
                {
                    _objectTag = _objectHit.collider.tag; // Получаем у объекта куда попал наш луч тэг

                    _rayHitGameObject = _objectHit.collider.gameObject; // Укорачиваю чтобы постоянно не использовать  _objectHit.collider.gameObject

                    if (IsTagInList) // Проверяем есть ли тэг объекта в тэг листе если да идем дальше
                    {
                        //DoorInteraction();
                    } // Проверяем есть ли тэг объекта в тэг листе если да идем дальше
                }
            }
        }
        private bool IsTagInList // Есть ли тэг в нашем листе
        {
            get
            {

                for (int i = 0; i < _tagList.Length; i++) // Просматриваем все тэги
                {
                    if (_objectTag == _tagList[i]) // Смотрим соответствует ли хоть один тэг в массиве с тэгом на объекте в который попадает луч
                    {
                        return true;
                    }
                }

                return false; // Возвращаем значение если на объекте нет тэга которые есть в листе
            }
        }


        //Взаимодействия
        private void DoorInteraction() // Открытие или закрытие двери
        {
            Animator _doorAnimator;
            Animation _animNow = null;

            _doorAnimator = _rayHitGameObject.GetComponent<Animator>();

            _animNow["Open1"].time = 0.5f;
        }
    }
}