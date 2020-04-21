using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind : MonoBehaviour
{
    [SerializeField] private LSS.LSS_FrontEnd LseseScript;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LseseScript.Load("Example2_On");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LseseScript.Load("Example2_Off");
        }
    }
}
