using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceInteractionKrishka : MonoBehaviour
{
    [SerializeField] private Interaction interactionScript;
    [SerializeField] private LiquidController liquidContr;

    private void Start()
    {
        if (interactionScript.AdvAction == true)
        {
            liquidContr.enabled = true;
        }
        else
        {
            liquidContr.enabled = false;
        }
    }

    public void AdvInteract()
    {
        interactionScript.TakeUpObject.transform.GetChild(1).GetComponent<Animator>().SetBool("OpenOrClose", interactionScript.AdvAction);

        if (interactionScript.AdvAction == true)
        {
            liquidContr.enabled = true;
        }
        else
        {
            liquidContr.enabled = false;
        }


    }

}
