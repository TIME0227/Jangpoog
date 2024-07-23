using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStickyGoo : ItemBase
{
    private void Start()
    {
        StartCoroutine(FadeEffect.Fade(GetComponent<SpriteRenderer>(), 1, 0, 3, 7, () =>
        {
            Debug.Log("ssg 사라집니다.");
            Destroy(gameObject);
        }));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Monster"))
        {
            Debug.Log("공격력 증가");
            other.GetComponent<MonsterController>().stat.currentDamage *= 1.5f;

        }

        else if (other.CompareTag("Player"))
        {
            //...
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("공격력 원래대로");
            other.GetComponent<MonsterController>().stat.currentDamage /= 1.5f;

        }

        else if (other.CompareTag("Player"))
        {
            //...
        }
    }




}
