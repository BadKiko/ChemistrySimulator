using UnityEngine;

public class OptimizationScript : MonoBehaviour
{
    [SerializeField] private GameObject[] HideAndShowObjects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Hide Objects");
            HideAndShowObjects[0].SetActive(false);
            HideAndShowObjects[1].SetActive(false);
            HideAndShowObjects[2].SetActive(false);
            HideAndShowObjects[3].SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Show Objects");
            HideAndShowObjects[0].SetActive(true);
            HideAndShowObjects[1].SetActive(true);
            HideAndShowObjects[2].SetActive(true);
            HideAndShowObjects[3].SetActive(true);
        }
    }
}
