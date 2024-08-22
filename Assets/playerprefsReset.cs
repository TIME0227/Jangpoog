using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerprefsReset : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.HasKey("BgmVolume"));
    }
}
