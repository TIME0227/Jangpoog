using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public AudioClip audioClip;
    public string NextSceneName;

    [SerializeField] private GameObject flagEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //파티클 뿌리고
            Instantiate(flagEffect, transform.position, Quaternion.identity);
            
            //효과음 빰빠밤
            Managers.Sound.Play(audioClip);
            
            //씬 이동
            //Managers.Scene.LoadSceneAfterDelay(NextSceneName,2f);
            StartCoroutine(Managers.Scene.LoadSceneAfterDelay(NextSceneName, 1f));
        }
    }
    
}
