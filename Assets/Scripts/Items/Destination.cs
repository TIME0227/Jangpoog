using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public AudioClip audioClip;
    public string NextSceneName;

    [SerializeField] private GameObject flagEffect;
    private GameObject keyInfo;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (keyInfo == null)
            {
                keyInfo = Managers.UI.MakeWorldSpaceUI<UI_KeyInfo>(transform).gameObject;
            }
            //keyInfo 활성화
            keyInfo.SetActive(true);
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            //파티클 뿌리고
            Instantiate(flagEffect, transform.position, Quaternion.identity);
            
            //효과음 빰빠밤
            Managers.Sound.Play(audioClip);
            
            //씬 이동
            //Managers.Scene.LoadSceneAfterDelay(NextSceneName,1f);
            StartCoroutine(Managers.Scene.LoadSceneAfterDelay(NextSceneName, 0.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyInfo != null)
            {
                keyInfo.SetActive(false);
            }
        }
    }
}
