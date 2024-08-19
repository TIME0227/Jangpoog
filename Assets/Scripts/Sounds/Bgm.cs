using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    [SerializeField] private AudioClip bgmAudioClip;

    private void Start()
    {
        if (bgmAudioClip == null)
        {
            Debug.Log("No BGM Audio Clip!!");
            return;
        }
        Managers.Sound.Play(bgmAudioClip, Define.Sound.Bgm);
    }
}
