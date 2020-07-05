using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationScript : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnBecameVisible()
    {
        this.gameObject.SetActive(true);
    }
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
