using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidMark : MonoBehaviour
{
    [SerializeField] GameObject flask, objectHelpWithAngle;
    RaycastHit DownRayHit, LerpHit;


    private void Start()
    {
        flask = transform.parent.GetChild(0).gameObject;
        objectHelpWithAngle = transform.parent.GetChild(0).GetChild(3).gameObject;
    }
    public void ShowMark()
    {
        this.gameObject.SetActive(true); // Скрипт находится на объекте сразу, поэтому не указываю

        Vector3 BeginLerp = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z); // Получение всех координ с самой метки для перетаскивания
       
        AngleCalculation();

        Vector3 StartRay = new Vector3(objectHelpWithAngle.transform.position.x, objectHelpWithAngle.transform.position.y - 0.3f, objectHelpWithAngle.transform.position.z);
        Vector3 FinishRay = Vector3.down;


        if (Physics.Raycast(StartRay, FinishRay, out DownRayHit, 10))
        {
            if (DownRayHit.collider.gameObject.tag != "NotMark")
            {
                Debug.Log("With Check - " + DownRayHit.collider.gameObject.name);
                Vector3 FinishUpLerp = new Vector3(this.gameObject.transform.position.x, DownRayHit.point.y + 0.01f, this.gameObject.transform.position.z);

                this.gameObject.transform.position = Vector3.Lerp(BeginLerp, FinishUpLerp, 0.2f);
            }
        }
        
        Debug.Log( "Without Check - " + DownRayHit.collider.gameObject.name);
        Debug.DrawRay(StartRay, FinishRay, Color.green);
    }


    public void AngleCalculation()
    {
        Vector3 BeginAngleRay = new Vector3(objectHelpWithAngle.transform.position.x, objectHelpWithAngle.transform.position.y, objectHelpWithAngle.transform.position.z);

        Vector3 FinishAngleRay = Vector3.down;

        Vector3 BeginLerp = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z); // Получение всех координ с самой метки для перетаскивания

        if (Physics.Raycast(BeginAngleRay, FinishAngleRay, out LerpHit, 10))
        {
            this.gameObject.transform.position = Vector3.Lerp(BeginLerp, LerpHit.point, 0.5f);
        }


        Debug.DrawRay(BeginAngleRay, FinishAngleRay, Color.red);
    }

    public void UnshowMark()
    {
        this.gameObject.SetActive(false); 
    }
}
