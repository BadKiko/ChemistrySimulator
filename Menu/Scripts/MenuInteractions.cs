using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    private Ray MainRay;
    private RaycastHit InteractRayHit;
    private Camera MainCamera;


    private void Start()
    {
        MainCamera = Camera.main;
    }
    private void Update()
    {
        MainRay = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(MainRay, out InteractRayHit, 100f))
        {
                if (InteractRayHit.collider.tag == "NewGameBtn" && Input.GetMouseButtonDown(0))
                {
                Application.LoadLevel(1);
                }
        }
    }
}
