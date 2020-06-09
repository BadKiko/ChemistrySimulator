using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Rigidbody PickUpObjectRigidbody;
    public float PickUpLerpSpeed, MouseScroll;
    [SerializeField] private GameObject DedicatedDoor, Pers;
    public GameObject PickUpObject;
    private Animator DedicatedDoorAnimator;

    [SerializeField] private Camera MainCamera;
    private Vector3 StartRay, FinishRay;
    private RaycastHit InteractRayHit;

    public KeyCode InteractionKey = KeyCode.E;
    public KeyCode AdvanceInteractionKey = KeyCode.F;

    [SerializeField] private bool DoorState = false; // Что делать с дверью открыть или закрыть
    public bool PickUpState = false, AdvAction = false; // Что делать с предметом

    [SerializeField] private float RotateSpeed;

    private void Start()
    {

    
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


                if (InteractRayHit.collider.tag == "Door" && InteractRayHit.distance <= 2f)
                {
                    DoorInteractions();
                }

                if (InteractRayHit.collider.tag == "CanMove" && InteractRayHit.distance <= 4f)
                {
                    ObjectInteracrion();
                }

            }
        }
        Debug.DrawLine(StartRay, FinishRay, Color.green);
    }

    private void FixedUpdate()
    {
        if (PickUpState == true)
        {
            if (Input.GetMouseButton(1))
            {
                Pers.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
                PickUpObjectRigidbody.useGravity = false;

                float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
                float rotY = Input.GetAxis("Mouse Y") * RotateSpeed * Mathf.Deg2Rad;

                PickUpObject.transform.RotateAround(Vector3.up, -rotX);
                PickUpObject.transform.RotateAround(Vector3.right, rotY);
            }
            else
            {
                Pers.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;

                PickUpObjectRigidbody.useGravity = false;
                PickUpObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                //PickUpObjectRigidbody.isKinematic = true;
                PickUpObject.transform.position = Vector3.Lerp(PickUpObject.transform.position, MainCamera.ScreenToWorldPoint(Input.mousePosition) + MainCamera.transform.TransformDirection(Vector3.forward) * MouseScroll, PickUpLerpSpeed);

            }
        }
        else
        {
            PickUpObjectRigidbody.constraints = RigidbodyConstraints.None;
            PickUpObjectRigidbody.useGravity = true;
            //PickUpObjectRigidbody.isKinematic = false;
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
}