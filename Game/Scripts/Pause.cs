using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _escPress = false;
    [SerializeField] private GameObject VFX_Smoke; 
    [SerializeField] private GameObject Pers, CameraController;
    [SerializeField] private GameObject PauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _escPress = !_escPress;

            Debug.Log(_escPress);
            if (_escPress == true)
            {
                VFX_Smoke.SetActive(true);
                VFX_Smoke.GetComponent<UnityEngine.Experimental.VFX.VisualEffect>().playRate = 0.25f;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Pers.GetComponent<CMF.Mover>().enabled = false;
                Pers.GetComponent<CMF.AdvancedWalkerController>().enabled = false;
                CameraController.GetComponent<CMF.CameraController>().enabled = false;

                PauseMenu.SetActive(true);
                PauseMenu.GetComponent<Animator>().SetBool("Show", true);
            }
            if (_escPress == false)
            {
                VFX_Smoke.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Pers.GetComponent<CMF.Mover>().enabled = true;
                Pers.GetComponent<CMF.AdvancedWalkerController>().enabled = true;
                CameraController.GetComponent<CMF.CameraController>().enabled = true;

                PauseMenu.SetActive(false);
                PauseMenu.GetComponent<Animator>().SetBool("Show", false);
            }

        }
    }
}
