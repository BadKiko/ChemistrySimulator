using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugConsole : MonoBehaviour
{
    [SerializeField] private Shader LiquidShader;
    [SerializeField] private Color LiquidColor;
    [SerializeField] GameObject EmptyBottle;
    [SerializeField] private Renderer _liqRender;
    private GameObject _instantieObject;
    [SerializeField] private Obi.ObiBaseFluidRenderer obi_bfr;

    [SerializeField] private InputField ie_field;
    [SerializeField] private Button btn_field;

    bool n_active;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            n_active = !n_active;

            if (n_active) 
            {
                ie_field.gameObject.SetActive(true);
                btn_field.gameObject.SetActive(true);

                Cursor.lockState = CursorLockMode.None; ;
                Cursor.visible = true;
            }
            else
            {
                ie_field.gameObject.SetActive(false);
                btn_field.gameObject.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }



            //_instantieObject = Instantiate(EmptyBottle);
            //_instantieObject.transform.position = new Vector3(-0.26198f, 1.1f, 0.1752f);
            //_liqRender = _instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>();
            //_liqRender.material = new Material(LiquidShader);
            //_liqRender.sharedMaterial.SetColor("_Tint", LiquidColor);
            //_liqRender.sharedMaterial.SetFloat("_FillAmount", 0.68f);
            //_instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = _liqRender.material;
            //_instantieObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().sharedMaterial = _liqRender.sharedMaterial;

            //obi_bfr.particleRenderers.Add(_instantieObject.transform.GetChild(0).GetChild(2).GetComponent<Obi.ObiParticleRenderer>());
            //Debug.Log("Created new object: " + _instantieObject.name);
            
        }
    }
}
