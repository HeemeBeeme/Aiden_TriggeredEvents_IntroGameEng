using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Hospital_Menu", LoadSceneMode.Single);
    }
}
