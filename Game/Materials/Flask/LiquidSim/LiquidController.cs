using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    private float nowObjectRotate;
    [Range(0, 180f)] public float RotateMin;
    [SerializeField] private GameObject obiSolverObject;
    [SerializeField] private Obi.ObiSolver mainObiSolver;
    private Renderer LiquidRenderer;
    [SerializeField] private float LiquidAmmount;

    private void Start()
    {
        LiquidRenderer = transform.GetChild(1).GetComponent<Renderer>();
    }
    void Update()
    {
        nowObjectRotate = transform.eulerAngles.x;

        if (nowObjectRotate <= RotateMin)
        {
            LiquidAmmount = LiquidRenderer.sharedMaterial.GetFloat("_FillAmount");

            LiquidAmmount += 0.01f;

            LiquidRenderer.sharedMaterial.SetFloat("_FillAmount", LiquidAmmount);


            if (LiquidAmmount <= 8.5f)
            {
                obiSolverObject.SetActive(true);
                mainObiSolver.enabled = true;
            }
        }
        else
        {
            obiSolverObject.SetActive(false);
            mainObiSolver.enabled = false;
        }

    }
}
