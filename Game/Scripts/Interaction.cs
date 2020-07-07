using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{



    [SerializeField] private ElementsColor ELMColor; 

    public Rigidbody PickUpObjectRigidbody;
    public float PickUpLerpSpeed, MouseScroll;
    [SerializeField] private GameObject DedicatedDoor, Pers, CameraController;
    public GameObject PickUpObject;
    public Obi.ObiEmitter PickUpObiEmitter;
    public Obi.ObiSolver PickUpObiSolver;
    private Animator DedicatedDoorAnimator;

    [SerializeField] private Camera MainCamera;
    [SerializeField] Vector3 LerpPickObject;
    private Vector3 StartRay, FinishRay;
    private RaycastHit InteractRayHit;

    public KeyCode InteractionKey = KeyCode.E;
    public KeyCode AdvanceInteractionKey = KeyCode.F;

    [SerializeField] private bool DoorState = false; // Что делать с дверью открыть или закрыть
    public bool PickUpState = false, AdvAction = false; // Что делать с предметом
    private bool _pickUpMode = true;

    [SerializeField] private float RotateSpeed;

    [SerializeField] private FluidRecognition _frScript;




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
                }

                if (InteractRayHit.collider.tag == "CanMove" && InteractRayHit.distance <= 4f) // Тэг колб и то что можно перетащить
                {
                    ObjectInteracrion();
                }

                if (InteractRayHit.collider.tag == "Kran" && InteractRayHit.distance <= 5f) // Тэг крана от куда берется вода
                {
                    KranInteraction();
                }
            }
        }
        Debug.DrawLine(StartRay, FinishRay, Color.green);
    }

    private void FixedUpdate()
    {
        if (PickUpState == true)
        {
            PickUpObject.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false; // Включает коллайдер куда попадает жидкость чтобы пополнить колбу



            if (Input.GetMouseButton(1)) // Вращение предмета в руке
            {
                Pers.GetComponent<CMF.Mover>().enabled = false;
                Pers.GetComponent<CMF.AdvancedWalkerController>().enabled = false;
                CameraController.GetComponent<CMF.CameraController>().enabled = false;

                PickUpObjectRigidbody.isKinematic = true;

                float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
                float rotY = Input.GetAxis("Mouse Y") * RotateSpeed * Mathf.Deg2Rad;

                PickUpObject.transform.RotateAround(Vector3.up, -rotX);
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
                LerpPickObject = MainCamera.ScreenToWorldPoint(Input.mousePosition) + (MainCamera.transform.TransformDirection(Vector3.forward) / 1.5f + MainCamera.transform.TransformDirection(Vector3.right) / 2f + MainCamera.transform.TransformDirection(Vector3.down) / 3) * 1.5f;
            }
        }
        else
        {
            PickUpObject.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            PickUpObjectRigidbody.constraints = RigidbodyConstraints.None;
            PickUpObjectRigidbody.useGravity = true;
            //PickUpObjectRigidbody.isKinematic = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            _pickUpMode = !_pickUpMode;
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
            PickUpObjectRigidbody = InteractRayHit.collider.gameObject.GetComponent<Rigidbody>();
            PickUpObiEmitter = PickUpObject.transform.GetChild(2).GetComponent<Obi.ObiEmitter>();
            PickUpObiSolver = PickUpObject.transform.parent.GetComponent<Obi.ObiSolver>();
            _frScript.OnEnable();
            PickUpState = !PickUpState;
        }
        if (Input.GetKeyDown(AdvanceInteractionKey))
        {
            PickUpObject = InteractRayHit.collider.gameObject;
            if(PickUpObject.GetComponent<AdvanceInteractionKrishka>())
            {
                AdvAction = !AdvAction;
                PickUpObject.GetComponent<AdvanceInteractionKrishka>().AdvInteract();
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
}