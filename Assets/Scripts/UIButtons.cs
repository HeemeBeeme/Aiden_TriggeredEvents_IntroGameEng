using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public bool SettingsMenuActivity = false;
    public GameObject SettingsMenuObj;
    public GameObject MainUI;
    public GameObject PlayUI;
    public GameObject PauseText;

    public void StartGame()
    {
        SceneManager.LoadScene("Hospital_Level", LoadSceneMode.Single);
    }

    public void SettingsMenu()
    {
        if(!SettingsMenuActivity)
        {
            SettingsMenuObj.SetActive(true);
            PauseText.SetActive(false);
            SettingsMenuActivity = true;
        }
        else
        {
            SettingsMenuObj.SetActive(false);
            PauseText.SetActive(true);
            SettingsMenuActivity = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UnpauseGame()
    {
        PlayUI.SetActive(true);
        MainUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Controller.Instance.m_IsPaused = false;
    }

    public void MenuButton()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Hospital_Menu", LoadSceneMode.Single);
    }
}
