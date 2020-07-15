using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStoneSys : MonoBehaviour
{
    [SerializeField] GameObject stone, liquid;
    [SerializeField] private float Wobble;
    [SerializeField] private Renderer _liqRender;
    public float Shit, Shit2;
    private void Update()
    {

        Vector3 BeginLerp = stone.transform.localPosition;
        Vector3 FinishLerp = new Vector3(stone.transform.localPosition.x, stone.transform.localPosition.y, Vector3.forward.y * 0.02f);

        if (_liqRender.material.GetFloat("_FillAmount") < 0.5f)
        {
            Shit = 0.01f;
            stone.transform.localPosition = new Vector3(0, 0, _liqRender.material.GetFloat("_FillAmount") * Shit);
            stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
            stone.transform.localPosition = Vector3.Lerp(FinishLerp, BeginLerp, 0.1f);
        }
        else if (_liqRender.material.GetFloat("_FillAmount") == 0.5f)
        {
            stone.transform.localPosition = new Vector3(0, 0, 0);
            stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
        }
        else
        {
            Shit = -0.01f;
            stone.transform.localPosition = new Vector3(0, 0, _liqRender.material.GetFloat("_FillAmount") * Shit);
            stone.transform.localPosition = Vector3.Lerp(BeginLerp, FinishLerp, 0.1f);
            stone.transform.localPosition = Vector3.Lerp(FinishLerp, BeginLerp, 0.1f);

        }


        Debug.Log(_liqRender.material.GetFloat("_FillAmount"));
    }
}
