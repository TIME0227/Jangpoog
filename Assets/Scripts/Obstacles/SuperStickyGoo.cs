using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStickyGoo : MonoBehaviour
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
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<MonsterController>().stat.currentDamage *= 1.5f;
            other.transform.localScale *= 1.2f;

        }

        else if (other.CompareTag("Player"))
        {
            MovementRigidbody2D playerMovement = other.GetComponent<MovementRigidbody2D>();
            playerMovement.IsInSsg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<MonsterController>().stat.currentDamage /= 1.5f;
            other.transform.localScale /= 1.2f;

        }

        else if (other.CompareTag("Player"))
        {
            MovementRigidbody2D playerMovement = other.GetComponent<MovementRigidbody2D>();
            playerMovement.IsInSsg = false;

        }
    }

}
