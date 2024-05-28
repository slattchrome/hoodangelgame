using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
