using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceInteractionKrishka : MonoBehaviour
{
    [SerializeField] private Interaction interactionScript;
    public void AdvInteract()
    {
        interactionScript.PickUpObject.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenOrClose", interactionScript.AdvAction);
    }
}
