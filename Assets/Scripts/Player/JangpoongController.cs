using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JangpoongController : MonoBehaviour
{  
    private Animator animator;
    private Collider2D collider2D;
    private Rigidbody2D rb;
    [SerializeField] private float damage = 0.5f;
    
    //1. 충돌
    //충돌 여부에 따라 애니메이션 조절
    //충돌시 폭파 애니메이션, 충돌 x시 일정 시간이 지나면 사라지도록
    //충돌 판정 : 몬스터, ground, level1, leveln, wall 충돌 시 폭파 처리 / 몬스터 충돌 시 몬스터 피격 처리
    //폭발할 때 collider 끄기?

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        //장풍 일시정지
        rb.velocity = new Vector2(0,0);
        collider2D.enabled = false;
        animator.SetTrigger("Explosion");

        if (other.gameObject.layer == (int)Define.Layer.Monster)
        {
            other.GetComponent<MonsterStat>().OnAttacked(damage);
        }
    }

    public void JangpoongVanish()
    {
        Destroy(gameObject);
    }


    //2. 장풍 데미지, 레벨 정보? 이걸 어디서 관리할 것이냐. playercontroller에서 장풍 생성할 때 값을 넘겨줄 것이냐, 아니면 어쩔 것이냐???????
    
    //3. 좌우 이동에 따라 장풍 좌우 반전 처리 -> scale로 처리? 아니면 filp으로 처리하는게 좋을까?
    
    
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
