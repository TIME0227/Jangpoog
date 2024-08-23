using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_SettingScene : MonoBehaviour
{
    public void ExitFromSettingScene()
    {
        if (string.IsNullOrEmpty(Managers.Scene.BeforeScene))
        {
            Managers.Scene.LoadScene("Main");
        }
        else
        {
            Managers.Scene.LoadScene(Managers.Scene.BeforeScene);
        }
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
}
