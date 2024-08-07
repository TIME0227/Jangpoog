using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterWeaponCollider : MonoBehaviour
{
    private Collider2D weaponCollider;
    private MonsterController monster;
    public Vector2 boxSize;
    private float damage;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        monster = GetComponentInParent<MonsterController>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (monster.State == Define.State.Attack)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("몬스터가 플레이어를 공격합니다.");
                    PlayerDataManager playerDataManager = Util.FindChild<PlayerDataManager>(collider.gameObject, null, true);
                    playerDataManager.OnAttacked(GetComponentInParent<MonsterStat>().monsterData.Damage);
                    return;
                }

            }
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("Player hit by weapon!");
    //         // 플레이어의 HP를 깎는 로직
    //         
    //         
    //     }
    // }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,boxSize);

    }
}
