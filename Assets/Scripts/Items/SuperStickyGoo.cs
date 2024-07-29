using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStickyGoo : ItemBase
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(FadeEffect.Fade(spriteRenderer, 1, 0, 3, 7, () =>
        {
            Debug.Log("ssg 사라집니다.");
            Destroy(transform.parent.gameObject);
        }));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Monster"))
        {
            Debug.Log("공격력, 사이즈 증가");
            other.GetComponent<MonsterController>().stat.currentDamage *= 1.5f;
            other.transform.localScale *= 1.2f;

        }

        else if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 속도 감소");
            //speed가 private라서 프로퍼티 필요
            //other.GetComponent<MovementRigidbody2D>().speed
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("공격력, 사이즈 원래대로");
            other.GetComponent<MonsterController>().stat.currentDamage /= 1.5f;
            other.transform.localScale /= 1.2f;

        }

        else if (other.CompareTag("Player"))
        {
            //...
        }
    }




}
