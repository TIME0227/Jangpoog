using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GamePopUp : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingUI;
    

    private void Start()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(false);
    }


    #region MyRegion
        public void OnPause()
        {
            SetTimeScale(0);
            playerUI.SetActive(false);
            pauseUI.SetActive(true);
        }

        public void OnResume()
        {
            SetTimeScale(1);
            pauseUI.SetActive(false);
            playerUI.SetActive(true);
        }

        public void OnExitGame()
        {
            Debug.Log("exit scene으로");
            Managers.Scene.LoadScene("Exit");
        }
        
    

    #endregion

    

    #region GameControl

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    #endregion
}
