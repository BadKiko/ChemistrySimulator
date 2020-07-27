using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Interaction : MonoBehaviour
{



    [SerializeField] private ElementsColor ELMColor; 

    public Rigidbody PickUpObjectRigidbody;
    public float PickUpLerpSpeed, MouseScroll;
    [SerializeField] private GameObject DedicatedDoor, Pers, CameraController;
    public GameObject PickUpObject, TakeUpObject; // TakeUpObject Это второстепенный объект с которым мы взаимодействуем на клавишу r
    public Obi.ObiEmitter PickUpObiEmitter;
    public Obi.ObiSolver PickUpObiSolver;
    private Animator DedicatedDoorAnimator;

    [SerializeField] private Camera MainCamera;
    [SerializeField] Vector3 LerpPickObject;
    private Vector3 StartRay, FinishRay;
    private RaycastHit InteractRayHit;

    public KeyCode InteractionKey = KeyCode.E;
    public KeyCode AdvanceInteractionKey = KeyCode.F;
    public KeyCode TakeInObjectKey = KeyCode.R;

    [SerializeField] private bool DoorState = false; // Что делать с дверью открыть или закрыть
    public bool PickUpState = false, AdvAction = false; // Что делать с предметом
    private bool _pickUpMode = true;
    private bool _takeUpMode = true;

    private int KolvoStoneInPincet;

    [SerializeField] private float RotateSpeed;

    [SerializeField] private FluidRecognition _frScript;

    [SerializeField] FluidMark fluidMark_Script;

    private GameObject KusokMetall; // кусок металла который будет в пинцете

    [SerializeField] private ElementIndification EI_Script; // Скрипт который смотрит за каждым элементом
    [SerializeField] private GridResize _gridResize; // скрипт ресайз грида
    [SerializeField] private TipsOnObject _objectTips;

    [SerializeField] private GameObject _LineOfTip;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        StartRay = MainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
        FinishRay = MainCamera.transform.TransformDirection(Vector3.forward) * 4;

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            if (MouseScroll > 3)
            {
                MouseScroll = 3;
            }
            else if (MouseScroll < 1.1f)
            {
                MouseScroll = 1.1f;
            }
            else
            {
                MouseScroll += Input.GetAxis("Mouse ScrollWheel");
            }
            
        }

        if (Physics.Raycast(StartRay, FinishRay, out InteractRayHit, 10))
        {
            if(InteractRayHit.collider != null)
            {


                if (InteractRayHit.collider.tag == "Door" && InteractRayHit.distance <= 2f) // Тэг дверей
                {
                    DoorInteractions();
                    _LineOfTip.GetComponent<Animator>().SetBool("ShowOrHide", true);

                    _objectTips = InteractRayHit.collider.gameObject.GetComponent<TipsOnObject>();
                    _objectTips.TipsMassive();
                }

                else if (InteractRayHit.collider.tag == "CanMove" && InteractRayHit.distance <= 4f) // Тэг колб и то что можно перетащить
                {
                    ObjectInteracrion();
                    _LineOfTip.GetComponent<Animator>().SetBool("ShowOrHide", true);

                    _objectTips = InteractRayHit.collider.gameObject.GetComponent<TipsOnObject>();
                    _objectTips.TipsMassive();
                }

                else if(InteractRayHit.collider.tag == "Kran" && InteractRayHit.distance <= 5f) // Тэг крана от куда берется вода
                {
                    KranInteraction();
                    _LineOfTip.GetComponent<Animator>().SetBool("ShowOrHide", true);

                    _objectTips = InteractRayHit.collider.gameObject.GetComponent<TipsOnObject>();
                    _objectTips.TipsMassive();
                }

                else if(InteractRayHit.collider.tag == "MoveOnlyPincet" && InteractRayHit.distance <= 8f) // Тэг крана от куда берется вода
                {
                    TakeUpStone();
                    _LineOfTip.GetComponent<Animator>().SetBool("ShowOrHide", true);

                    _objectTips = InteractRayHit.collider.gameObject.GetComponent<TipsOnObject>();
                    _objectTips.TipsMassive();
                }

                else
                {
                    _LineOfTip.GetComponent<Animator>().SetBool("ShowOrHide", false);
                }
            }

        }
        Debug.DrawLine(StartRay, FinishRay, Color.green);
    }

    private void FixedUpdate()
    {
        if (PickUpState == true)
        {
            _LineOfTip.SetActive(false);


            PickUpObject.transform.position = Vector3.Lerp(PickUpObject.transform.position, LerpPickObject, PickUpLerpSpeed);

            if (PickUpObject.name == "flask")
            {
                PickUpObject.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false; // Включает коллайдер куда попадает жидкость чтобы пополнить колбу

                fluidMark_Script.ShowMark(); // Показывает пометку куда лить
            }


            if (Input.GetMouseButton(1)) // Вращение предмета в руке
                {

                    Pers.GetComponent<CMF.Mover>().enabled = false;
                    Pers.GetComponent<CMF.AdvancedWalkerController>().enabled = false;
                    CameraController.GetComponent<CMF.CameraController>().enabled = false;

                    PickUpObjectRigidbody.isKinematic = true;

                    //float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
                    float rotY = Input.GetAxis("Mouse Y") * RotateSpeed * Mathf.Deg2Rad;

                    //PickUpObject.transform.RotateAround(Vector3.up, -rotX);
                    PickUpObject.transform.RotateAround(Vector3.right, rotY);

                }

                else
                {
                    Pers.GetComponent<CMF.Mover>().enabled = true;
                    Pers.GetComponent<CMF.AdvancedWalkerController>().enabled = true;
                    CameraController.GetComponent<CMF.CameraController>().enabled = true;


                    PickUpObjectRigidbody.useGravity = false;
                    PickUpObjectRigidbody.isKinematic = false;
                    PickUpObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    PickUpObject.transform.position = Vector3.Lerp(PickUpObject.transform.position, LerpPickObject, PickUpLerpSpeed);
                }

            if (_pickUpMode)
            {
                LerpPickObject = MainCamera.ScreenToWorldPoint(Input.mousePosition) + MainCamera.transform.TransformDirection(Vector3.forward) * MouseScroll;
            }
            else
            {
                if (PickUpObject.name == "flask")
                {
                    fluidMark_Script.UnshowMark(); // Убирает пометку куда лить
                }
                LerpPickObject = MainCamera.ScreenToWorldPoint(Input.mousePosition) + (MainCamera.transform.TransformDirection(Vector3.forward) / 1.5f + MainCamera.transform.TransformDirection(Vector3.right) / 2f + MainCamera.transform.TransformDirection(Vector3.down) / 3) * 1.5f;
            }
        }
        else
        {
            _LineOfTip.SetActive(true);

            if (PickUpObject.name == "flask")
            {
                fluidMark_Script.UnshowMark(); // Убирает пометку куда лить

                PickUpObject.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            }
            PickUpObjectRigidbody.constraints = RigidbodyConstraints.None;
            PickUpObjectRigidbody.useGravity = true;
            //PickUpObjectRigidbody.isKinematic = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            _pickUpMode = !_pickUpMode;
        }

        if (_takeUpMode)
        {
            Vector3 StartLerp = KusokMetall.transform.position;
            Vector3 FinishLerp = PickUpObject.transform.GetChild(0).gameObject.transform.position;

            KusokMetall.transform.position = Vector3.Lerp(StartLerp, FinishLerp, 0.5f);
        }
    }


    public void DoorInteractions()
    {
        Debug.Log("InteractWithDoor");

        if (Input.GetKeyDown(InteractionKey))
        {
            
            if(DedicatedDoor == null || DedicatedDoor.name != InteractRayHit.collider.gameObject.name)
            {

                Debug.LogWarning("ChangeDoor");
                DedicatedDoor = InteractRayHit.collider.gameObject;

                DedicatedDoorAnimator = DedicatedDoor.transform.GetComponent<Animator>();

                DoorState = !DedicatedDoorAnimator.GetBool("DoorInteraction");

                DedicatedDoorAnimator.SetBool("DoorInteraction", DoorState);

                Debug.Log(DoorState);

            }
            else
            {
                DoorState = !DoorState;
                DedicatedDoorAnimator.SetBool("DoorInteraction", DoorState);
            }
            
        }

    }

    public void DoorShkaf()
    {
        Debug.Log("InteractWithDoorShkaf");

        if (Input.GetKeyDown(InteractionKey))
        {

            if (DedicatedDoor == null || DedicatedDoor.name != InteractRayHit.collider.gameObject.name)
            {

                Debug.LogWarning("ChangeDoorShkaf");
                DedicatedDoor = InteractRayHit.collider.gameObject;

                DedicatedDoorAnimator = DedicatedDoor.transform.GetComponent<Animator>();

                DoorState = !DedicatedDoorAnimator.GetBool("DoorInteraction");

                DedicatedDoorAnimator.SetBool("DoorInteraction", DoorState);

                Debug.Log(DoorState);

            }
            else
            {
                DoorState = !DoorState;
                DedicatedDoorAnimator.SetBool("DoorInteraction", DoorState);
            }

        }
    }

    public void ObjectInteracrion()
    {



        if (Input.GetKeyDown(InteractionKey))
        {
            PickUpObject = InteractRayHit.collider.gameObject;
            if (PickUpState == false)
            {
                if (PickUpObject.name == "flask")
                {
                    fluidMark_Script = PickUpObject.transform.parent.GetChild(1).GetComponent<FluidMark>();
                    PickUpObiEmitter = PickUpObject.transform.GetChild(2).GetComponent<Obi.ObiEmitter>();
                    PickUpObiSolver = PickUpObject.transform.parent.GetComponent<Obi.ObiSolver>();
                    _frScript.OnEnable();
                }

                PickUpObjectRigidbody = InteractRayHit.collider.gameObject.GetComponent<Rigidbody>();
            }
            PickUpState = !PickUpState;
        }


        if (Input.GetKeyDown(AdvanceInteractionKey))
        {
            TakeUpObject = InteractRayHit.collider.gameObject;
            if (TakeUpObject.GetComponent<AdvanceInteractionKrishka>())
            {
                AdvAction = !AdvAction;
                TakeUpObject.GetComponent<AdvanceInteractionKrishka>().AdvInteract();
       
            }
            if (TakeUpObject.name == "pincet")
            {

                GameObject stone;
                stone = PickUpObject.transform.GetChild(1).gameObject;

                stone.AddComponent<Rigidbody>();
                stone.AddComponent<BoxCollider>();

                stone.transform.SetParent(null);

                KolvoStoneInPincet = 0;

                _takeUpMode = false;

                Debug.Log("DA 2- " + TakeUpObject.name);

            }
        }


        if (Input.GetKeyDown(TakeInObjectKey))
        {
            TakeUpObject = InteractRayHit.collider.gameObject;
            if (TakeUpObject.name == "bankaWithMetall")
            {
                _takeUpMode = true;
                TakeMetallInBanka();
            }
        }
    }

    public void KranInteraction()
    {

        Debug.Log("Kran Interacted");

        if (Input.GetKeyDown(InteractionKey))
        {
            PickUpObject.transform.GetChild(1).GetComponent<Renderer>().sharedMaterial.SetFloat("_FillAmount", 0.4f);
            PickUpObject.transform.GetChild(1).GetComponent<Renderer>().sharedMaterial.SetColor("_Tint", ELMColor.Water);
        }
    }

    private void TakeMetallInBanka()
    {
        if (PickUpObject.name == "pincet")
        {
            if (KolvoStoneInPincet <= 0)
            {
                TakeUpObject.transform.GetChild(Random.Range(4, TakeUpObject.transform.childCount)).transform.SetParent(PickUpObject.transform);
                KusokMetall = PickUpObject.transform.GetChild(1).gameObject;

                EI_Script = KusokMetall.GetComponent<ElementIndification>();

                if (EI_Script.ElementIs == 2)
                {
                    EI_Script.InteractWithOxygen = true;
                }

                KolvoStoneInPincet = 1;
            }
        }
        
    }

    private void TakeUpStone()
    {
        if (Input.GetKeyDown(TakeInObjectKey))
        {

            TakeUpObject = InteractRayHit.collider.gameObject;

            if (TakeUpObject.tag == "MoveOnlyPincet")
            {
                _takeUpMode = true;
                Destroy(TakeUpObject.GetComponent<Rigidbody>());
                TakeUpObject.transform.SetParent(PickUpObject.transform);
                KusokMetall = PickUpObject.transform.GetChild(1).gameObject;

                EI_Script = KusokMetall.GetComponent<ElementIndification>();

                KolvoStoneInPincet = 1;

            }
        }
    }
}