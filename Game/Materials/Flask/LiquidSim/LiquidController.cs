using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    private float nowObjectRotate;
    [Range(0, 180f)] public float RotateMin, MinimumForShowMark;
    [SerializeField] private Obi.ObiEmitter mainObiEmiter;
    [SerializeField] private Obi.ObiSolver mainObiSolver;
    private Renderer LiquidRenderer;
    [SerializeField] private float LiquidAmmount;
    [SerializeField] private int WhatAChildren;
    [SerializeField] Interaction _interactionScript;


    private void Start()
    {
        LiquidRenderer = transform.GetChild(WhatAChildren).GetComponent<Renderer>();
        LiquidAmmount = LiquidRenderer.sharedMaterial.GetFloat("_FillAmount");

        _interactionScript = GameObject.Find("FirstPersonWalker_Audio").GetComponent<Interaction>(); // Не самое оптимизированное решение, но выполняется 1 раз для того чтобы референс который не имеет доступа к сцене при появлениии нашел скрипт
    }

    private void FixedUpdate()
    {
        //Чтобы вода не зависила от фпс.

        nowObjectRotate = transform.eulerAngles.x;

        if (nowObjectRotate <= RotateMin)
        {
            LiquidAmmount = LiquidRenderer.sharedMaterial.GetFloat("_FillAmount");
            _interactionScript.PickUpObject.transform.GetChild(WhatAChildren).GetComponent<BoxCollider>().enabled = false;

            

            if (LiquidAmmount <= 0.68f)
            {
                LiquidAmmount += 0.001f;
                LiquidRenderer.sharedMaterial.SetFloat("_FillAmount", LiquidAmmount);

                mainObiEmiter.GetComponent<Obi.ObiEmitter>().speed = 1.5f;
                mainObiSolver.GetComponent<Obi.ObiSolver>().enabled = true;
            }
            else
            {
                mainObiEmiter.GetComponent<Obi.ObiEmitter>().speed = 0;
            }
        }
        else
        {
            mainObiEmiter.GetComponent<Obi.ObiEmitter>().speed = 0;
        }
    }

    public void AddFluid(float _liquidPlusAmmount)
    {
        LiquidAmmount -= _liquidPlusAmmount;
        LiquidRenderer.sharedMaterial.SetFloat("_FillAmount", LiquidAmmount);
    }
}
