using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeaponCollider : MonoBehaviour
{
    private Collider2D weaponCollider;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by weapon!");
            // 플레이어의 HP를 깎는 로직
            
            
        }
    }
}
