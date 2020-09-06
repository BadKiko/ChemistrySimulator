using UnityEngine;
using UnityEngine.UI;

public class EscapeButtonManager : MonoBehaviour
{
    private bool _escPress = false;
    [SerializeField] private GameObject VFX_Smoke; 
    [SerializeField] private GameObject Pers, CameraController;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Button Exit, InMenu;
    [SerializeField] private ChemistrySimulator.Interaction interaction;

    private void Start()
    {
        Exit.onClick.AddListener(ExitGame);
        InMenu.onClick.AddListener(ExitInMenu);
    }

    void Update()
    {
        Debug.Log(interaction.isLaptopOpened + "sss");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _escPress = !_escPress;

            Debug.Log(_escPress);
            if (_escPress == true && interaction.isLaptopOpened == false)
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
            if (_escPress == false && interaction.isLaptopOpened == false)
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
            if (interaction.isLaptopOpened) // Если ноутбук открыт, то мы делаем не паузу а вызываем действие закрытия
            {
                interaction.CloseLaptop();
            }
        }
    }

    void ExitInMenu()
    {
        Application.LoadLevel(2);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
