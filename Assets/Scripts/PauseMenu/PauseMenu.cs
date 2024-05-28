using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject perkAndHealthUI;
    [SerializeField]
    GameObject statisticsSection;
    [SerializeField]
    GameObject exitSection;

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        perkAndHealthUI.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        perkAndHealthUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void Statistics()
    {
        pauseMenu.SetActive(false);
        statisticsSection.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        statisticsSection.SetActive(false);
        exitSection.SetActive(false);
    }

    public void ExitWarn()
    {
        pauseMenu.SetActive(false);
        exitSection.SetActive(true);
    }

    public void Exit()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
