using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugConsole : MonoBehaviour
{
    [SerializeField] private Shader LiquidShader;
    [SerializeField] private Color LiquidColor;
    [SerializeField] GameObject EmptyBottle;
    [SerializeField] private Renderer _liqRender;
    private GameObject _instantieObject;
    [SerializeField] private Obi.ObiBaseFluidRenderer obi_bfr;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {

            _instantieObject = Instantiate(EmptyBottle);
            _instantieObject.transform.position = new Vector3(-0.26198f, 1.1f, 0.1752f);
            _liqRender = _instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>();
            _liqRender.material = new Material(LiquidShader);
            _liqRender.sharedMaterial.SetColor("_Tint", LiquidColor);
            _liqRender.sharedMaterial.SetFloat("_FillAmount", 1f);
            _instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = _liqRender.material;
            _instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().sharedMaterial = _liqRender.sharedMaterial;

            obi_bfr.particleRenderers.Add(_instantieObject.transform.GetChild(0).GetChild(2).GetComponent<Obi.ObiParticleRenderer>());
            Debug.Log("Created new object: " + _instantieObject.name);
            
        }
    }
}
