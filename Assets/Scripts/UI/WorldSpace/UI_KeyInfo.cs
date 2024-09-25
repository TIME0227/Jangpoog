using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_KeyInfo : UI_Base
{
    [SerializeField] private GameObject KeyInfo;
    private float offset_y = 0.5f;


    [SerializeField] Monster monster;

    private float max; //slider max
    private float min; //slider min



    public override void Init()
    {
        //부모의 head position 가져오기
        Collider2D p_collider = GetComponentInParent<Collider2D>();
        Bounds p_bounds = p_collider.bounds;

        Vector2 p_headPosition = new Vector2(p_bounds.center.x, p_bounds.max.y);
        
        
        //UI_KeyInfo의 위치 설정
        transform.position = new Vector3(p_headPosition.x, p_headPosition.y+offset_y, transform.position.z);

    }
}
