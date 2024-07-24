using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; //카메라 추적 대상
    [SerializeField] bool x = true, y = true, z;  //추적 축
    float offsetY; //target과 카메라의 y 거리 값을 저장하는 변수

    // Start is called before the first frame update

    private void Awake()
    {
        //target 자동으로 player로 설정
        target = GameObject.FindWithTag("Player").transform;

        //offsetY 값 설정
        //target과 카메라의 y 거리 = |(카메라의 y 위치) - (tartget의 y 위치)|
        offsetY = Mathf.Abs(transform.position.y - target.position.y);
    }
    void Start()
    {

    }
    private void LateUpdate()
    {
        if (target == null) return;
        //true 축만 target의 좌표를 따라가도록 한다.
        //카메라의 위치는 x,y,z 각각 true면 target의 위치로, false면 카메라 위치 그대로
        //단 y 축은 target + offsetY
        transform.position = new Vector3(x ? target.position.x : transform.position.x, y ? target.position.y + offsetY : transform.position.y, z ? target.position.z : transform.position.z);


        //카메라의 좌/우측 이동 범위를 넘어가지 않도록 설정(추가 예정)

    }
}
