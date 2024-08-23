using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOverButtons : MonoBehaviour
{
    public Button restartButton;
    public Button exitButton;

    private void Start()
    {
        throw new NotImplementedException();
    }


    public void Restart()
    {
        Managers.Scene.LoadScene();
    }
}
