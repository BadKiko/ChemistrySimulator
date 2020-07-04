using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    [SerializeField] private GameObject OptionsPanel;
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

        if (Physics.Raycast(MainRay, out InteractRayHit, 100f))
        {
            if (InteractRayHit.collider.tag == "ExitButton" && Input.GetMouseButtonDown(0))
            {
                Application.Quit();
            }
        }

        if (Physics.Raycast(MainRay, out InteractRayHit, 100f))
        {
            if (InteractRayHit.collider.tag == "OptionButton" && Input.GetMouseButtonDown(0))
            {
                OptionsPanel.SetActive(true);
            }
        }
    }
}
