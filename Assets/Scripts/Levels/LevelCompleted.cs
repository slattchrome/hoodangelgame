using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    GameObject levelCompleted;
    [SerializeField]
    GameObject perksAndHealthUI;
    [SerializeField]
    GameObject pauseMenuUI;
    [SerializeField]
    private LevelSO completedLevelsCount;

    private float timeToTransition = 0;
    private int currentSceneIndex;
    private bool levelCompletedTriggered = false; 

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {

        if (completedLevelsCount.Value != 2)
        {
            if (GameObject.FindWithTag("Enemy") == null && !levelCompletedTriggered && GameObject.FindWithTag("Collectable") == null)
            {
                completedLevelsCount.Value++;

                levelCompletedTriggered = true; 
                perksAndHealthUI.SetActive(false);
                pauseMenuUI.SetActive(false);
                levelCompleted.SetActive(true);
                timeToTransition += Time.deltaTime;
            }

            if (levelCompletedTriggered)
            {
                timeToTransition += Time.deltaTime;
                if (timeToTransition > 5f)
                {
                    levelCompletedTriggered = false;
                    switch (currentSceneIndex)
                    {
                        case 1:
                            SceneManager.LoadScene(2);
                            break;
                        case 2:
                            SceneManager.LoadScene(1);
                            break;
                    }
                }
            }
        }
        else
        {
            timeToTransition += Time.deltaTime;
            if (timeToTransition > 5f)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(3);
            }
        }
    }
}
