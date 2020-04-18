using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCollider : MonoBehaviour
{
    [SerializeField] private Interaction InteractionScript;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Wall")
        {
            InteractionScript.MouseScroll -= 1f;

            if (InteractionScript.MouseScroll <= 0.1f)
            {
                InteractionScript.PickUpObjectRigidbody.constraints = RigidbodyConstraints.None;
                InteractionScript.PickUpObjectRigidbody.useGravity = true;
                InteractionScript.PickUpState = false;
             //   InteractionScript.PickUpObjectRigidbody.isKinematic = false;
                InteractionScript.MouseScroll = 2f;

            }
        }
    }
}
