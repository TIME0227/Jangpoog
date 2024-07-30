using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct BackgroundData
{
    public Renderer background;
    public float speed;
}

public class ParallaxBackground : MonoBehaviour
{
    //Parallax camera
    [SerializeField] private Transform targetCamera;
    //BackgroundData
    [SerializeField] private BackgroundData[] backgrounds; //플레이어 이동에 따라 연속되어 생성되는 배경
    [SerializeField] private BackgroundData[] backgroundClouds; //구름과 같이 시간의 흐름에 따라 스스로 이동하는 배경
    private float targetStartX; //target Camera의 시작 position x

    private void Awake()
    {
        targetStartX = targetCamera.position.x;
    }

    private void Update()
    {
        //구름 이동 처리
        foreach (BackgroundData bgCloud in backgroundClouds)
        {
            float cloudOffset = Time.time * bgCloud.speed;
            bgCloud.background.material.mainTextureOffset = new Vector2(cloudOffset, 0);

            if (targetCamera == null) return;
            float x = targetCamera.position.x - targetStartX;

            foreach (BackgroundData bg in backgrounds)
            {
                float offset = x * bg.speed;
                bg.background.material.mainTextureOffset = new Vector2(offset, 0);
            }
        }
        
        
        
    }
}
