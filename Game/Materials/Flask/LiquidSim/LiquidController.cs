using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    private float nowObjectRotate;
    [Range(0, 180f)] public float RotateMin;
    [SerializeField] private Obi.ObiEmitter mainObiEmiter;
    [SerializeField] private Obi.ObiSolver mainObiSolver;
    private Renderer LiquidRenderer;
    [SerializeField] private float LiquidAmmount;
    [SerializeField] private bool bug90angle;

    private void Start()
    {
        LiquidRenderer = transform.GetChild(1).GetComponent<Renderer>();

    }

    private void FixedUpdate()
    {
        //Чтобы вода не зависила от фпс.

        nowObjectRotate = transform.eulerAngles.x;

        if (bug90angle)
        {
            
        }


        if (nowObjectRotate <= RotateMin)
        {
            LiquidAmmount = LiquidRenderer.sharedMaterial.GetFloat("_FillAmount");


            if (LiquidAmmount <= 0.8f)
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
}
