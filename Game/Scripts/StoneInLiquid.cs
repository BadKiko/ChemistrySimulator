using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInLiquid : MonoBehaviour
{
    [Header("Объект который будет плавать в жидкости:")]
    public GameObject Stone;
    [Header("Шейдер жидкости:")]
    public Renderer LiquidRenderer;
    [Header("Число для управления заполненостью")]
    [SerializeField] private float LiquidControll;
    private void Update()
    {
        if (Stone != null && LiquidRenderer != null)
        {
            Vector3 BeginLerp = Stone.transform.localPosition;
            Vector3 FinishLerp = new Vector3(Stone.transform.localPosition.x, Stone.transform.localPosition.y, Vector3.forward.y * 0.02f);

            if (LiquidRenderer.material.GetFloat("_FillAmount") < 0.5f)
            {
                LiquidControll = 0.01f;
                Stone.transform.localPosition = new Vector3(0, 0, LiquidRenderer.material.GetFloat("_FillAmount") * LiquidControll);
                Stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
                Stone.transform.localPosition = Vector3.Lerp(FinishLerp, BeginLerp, 0.1f);
            }
            else if (LiquidRenderer.material.GetFloat("_FillAmount") == 0.5f)
            {
                Stone.transform.localPosition = new Vector3(0, 0, 0);
                Stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
            }
            else
            {
                LiquidControll = -0.01f;
                Stone.transform.localPosition = new Vector3(0, 0, LiquidRenderer.material.GetFloat("_FillAmount") * LiquidControll);
                Stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
                Stone.transform.localPosition = Vector3.Lerp(FinishLerp, BeginLerp, 0.1f);

            }
        }

        Debug.Log(LiquidRenderer.material.GetFloat("_FillAmount"));
    }
}
