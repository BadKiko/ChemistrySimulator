using UnityEngine;
using MyBox; // Атрибут для того чтобы скрывать другие атрибуты при bool
using System;

namespace ChemistrySimulator
{

    public class Interaction : MonoBehaviour
    {
        // Основное
        public KeyCode interactionKey = KeyCode.E; // Основная кнопка взаимодействия
        private GameObject _rayHitGameObject; // Объект в который попадает наш луч
        [Header("Камера игрока:")]
        [SerializeField] private Camera _mainCamera; // Главная камера которая стоит на игроке

        // Лучи
        private Vector3 _beginRay; // Начало луча
        private Vector3 _finishRay; // Конец луча

        RaycastHit _objectHit; // Куда попал луч

        // Тэги
        private int _tagPositionInTags = -1; // Позиция тэга с объекта в который попал луч в нашем массиве
        private string _objectTag; // Тэг с объекта на который попал луч
        [Header("Тэги которые участвуют в взаимодействиях")]
        [SerializeField] private string[] _tagList; // Тэги которые участвуют в взаимодействиях

        // PickUpObject
        private bool _pickUpped; // Бул то что объект поднят
        [SerializeField] [Range(0f, 10f)] float _lerpSpeed; // Скорость лерпа для поднимаемого объекта
        

        private void Start()
        {
            Cursor.visible = false; // Делаем курсор невидимым
            Cursor.lockState = CursorLockMode.Locked; // Блокировка мыши
        }
        private void Update()
        {
            _beginRay = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2)); // Получаем origin луча с помощью деления экрана
            _finishRay = _mainCamera.transform.TransformDirection(Vector3.forward); // Конец луча TransformDirection нужен чтобы луч всегда смотрел вперед не зваися от позиции объекта

            TakeRaycastHit(); // Вызываем рэйкаст

            if (_pickUpped) // Если какой то объект поднят повторяется множество раз
            {
                PickUpLerpObject(_rayHitGameObject); // Лерпаем на середину

                if (Input.GetMouseButton(1))
                {
                    Debug.Log("Work Once");
                    RotatePickUpObject(_rayHitGameObject, 180, 1);
                }
            }

            Debug.DrawRay(_beginRay, _finishRay * 6f, Color.red); // Линия куда 
        }
        private void TakeRaycastHit() // Смотрим куда попал наш луч
        {
            if (Physics.Raycast(_beginRay, _finishRay, out _objectHit, 4))
            {


                if (_objectHit.collider != null) // Смотрим чтобы наш коллайдер не был равен нулю
                {
                    _objectTag = _objectHit.collider.tag; // Получаем тэг у объекта куда попал наш луч

                    if (!_pickUpped) // Если ничего не поднято, то мы можем ставить новый предмет
                    {
                        _rayHitGameObject = _objectHit.collider.gameObject; // Укорачиваю чтобы постоянно не использовать  _objectHit.collider.gameObject
                    }

                    Debug.Log(IsTagInList);

                    if (IsTagInList) // Проверяем есть ли тэг объекта в тэг листе если да идем дальше
                    {
                        if (Input.GetKeyDown(interactionKey)) // Если нажата кнопка взаимодействия (по умолчанию это Е)
                        {
                            ChooseInteraction(); // По номеру в массиве делает взаимодействия
                        } // Если нажата кнопка взаимодействия (по умолчанию это Е)
                    } // Проверяем есть ли тэг объекта в тэг листе если да идем дальше
                }


            }
        }
        private bool IsTagInList // Есть ли тэг в нашем листе
        {
            get
            {
                _tagPositionInTags = -1; // Обнуляем чтобы при вызове всегда было -1

                foreach (string tag in _tagList) // Просматриваем все тэги
                {
                    _tagPositionInTags++; // Позиция тэга с объекта в тэг листе

                    if (_objectTag == tag) // Смотрим соответствует ли хоть один тэг в массиве с тэгом на объекте в который попадает луч
                    {
                        return true;
                    }
                }

                return false; // Возвращаем значение если на объекте нет тэга которые есть в листе

            }
        }

        private void ChooseInteraction()
        {

            /* Как добавить действия:
             * 
             * 1) В инспекторе добавляем тэг.
             * 2) В свитче добавляем кейс
             * 3) В кейсе указываем действие
             * 
             */

            switch (_tagPositionInTags)
            {
                case 0:
                    DoorInteraction();
                    break;
                case 1:
                    PickUpInteraction();
                    break;

                default:
                    Debug.LogError("Can't find a interaction!");
                    break;
            }
        }

        // Взаимодействия
        private void DoorInteraction() // Открытие или закрытие двери
        {
            bool _nowBool; // Бул для того чтобы брать с аниматора и инвертировать его значения для открытия и закрытия

            _nowBool = !_rayHitGameObject.GetComponent<Animator>().GetBool("DoorInteraction"); // Получаем значения була с аниматора по состоянию двери и сразу же инвертируем (  _nowBool = !_nowBool; // Инвертируем )

            _rayHitGameObject.GetComponent<Animator>().SetBool("DoorInteraction", _nowBool); // Ставим инвертированное значение в наш аниматор двери

        }
        
        // PickUpObject функции
        private void PickUpInteraction() // Основа пикапа, выполняется лишь 1 раз
        {
            _pickUpped = !_pickUpped; // Переключаем из есть что-то в руке и ничего нет

            RigidbodyControll(_rayHitGameObject, !_pickUpped);

            PickUpLerpObject(_rayHitGameObject);
        }
        private void PickUpLerpObject(GameObject gameObject) // Функция лерпа объекта к центру экрана
        {
            Vector3 _beginLerp = gameObject.transform.position; // Начальная точка это точка объекта в который попал луч
            Vector3 _finishLerp = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0)) + _mainCamera.transform.TransformDirection(Vector3.forward) * 2f; // Начальная точка это точка объекта в который попал луч

            gameObject.transform.position = Vector3.Lerp(_beginLerp, _finishLerp, _lerpSpeed);
        }
        private void RigidbodyControll(GameObject gameObject, bool isGravity) // Выключение и включение основных функций физики для взаимодействий с предметами
        {
            Rigidbody gameObjectRigidbody = gameObject.GetComponent<Rigidbody>();

            gameObjectRigidbody.useGravity = isGravity;
        }

        private void RotatePickUpObject(GameObject rotateGameObject, float rotateEulerX, float speedRotate) // Поворот объекта на нажатие ПКМ
        {
            Vector3 endRotateVector = new Vector3(rotateEulerX, rotateGameObject.transform.eulerAngles.y, rotateGameObject.transform.eulerAngles.z); // Тот вектор вращения который должен достигнуть в конце объект
            rotateGameObject.transform.eulerAngles = Vector3.Lerp(rotateGameObject.transform.eulerAngles, endRotateVector, speedRotate);
        }

    }
}