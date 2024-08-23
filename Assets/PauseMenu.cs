using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject PlayerUI;

   void Start()
    {
        //처음 씬을 오픈하면 pause 메뉴가 안 뜸
        // GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }
    

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (GameIsPaused)
    //         {
    //             Resume();
    //         }
    //         else
    //         {
    //             Pause();
    //         }
    //     }
    // }

    public void OnButtonPause()
    {
        pauseMenuUI.SetActive(true);
        PlayerUI.SetActive(false);
    }
    
    public void OnClickResume()
    {
        pauseMenuUI.SetActive(false); 
        PlayerUI.SetActive(true);
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerUI.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PlayerUI.SetActive(false);
    }
    
}
