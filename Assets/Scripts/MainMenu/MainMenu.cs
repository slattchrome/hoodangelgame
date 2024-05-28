using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject controlsSection;
    [SerializeField]
    GameObject levelsSection;
    [SerializeField]
    GameObject settingsSection;


    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        controlsSection.SetActive(true);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        controlsSection.SetActive(false);
        levelsSection.SetActive(false);
        settingsSection.SetActive(false);
    }

    public void SelectLevel() 
    {
        PlayerPrefs.DeleteKey("perksCount");
        PlayerPrefs.DeleteKey("killsCount");
        PlayerPrefs.DeleteKey("deathsCount");
        mainMenu.SetActive(false);
        levelsSection.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsSection.SetActive(true);
    }

    public void Exit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
