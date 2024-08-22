using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterWeaponCollider : MonoBehaviour
{
    public Vector2 boxSize;
    private float damage;

    void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            Managers.PlayerData.OnAttacked(GetComponentInParent<MonsterStat>().monsterData.Damage);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,boxSize);

    }


    public void AttackPlayerByWeapon()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.name);
            if (collider.CompareTag("Player"))
            {
                Debug.Log("몬스터가 플레이어를 공격합니다.");
                // PlayerDataManager playerDataManager = Util.FindChild<PlayerDataManager>(collider.gameObject, null, true);
                
                Managers.PlayerData.OnAttacked(GetComponentInParent<MonsterStat>().monsterData.Damage);
                // playerDataManager.OnAttacked(GetComponentInParent<MonsterStat>().monsterData.Damage);
                return;
            }
    
        }

    }
}
