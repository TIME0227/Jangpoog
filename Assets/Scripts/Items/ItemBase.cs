using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnForce = new Vector2(1, 7); // 아이템 생성 시 이동 힘
    [SerializeField]
    private float aliveTImeAfterSpwn = 5;                   // 아이템 생성 후 스폰 시간

    private bool allowCollect = true;                           // 아이템 획득 가능 여부

    public Define.Item itemType; // 아이템 타입

    public void Setup()                                                    
    {
        // 아이템 생성 코루틴 메소드 호출
        StartCoroutine(nameof(SpawnItemProcess));
    }

    // 아이템 생성
    private IEnumerator SpawnItemProcess()
    {
        allowCollect = false;

        var rigid = gameObject.AddComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.velocity = new Vector2(Random.Range(-spawnForce.x, spawnForce.x), spawnForce.y);

        while (rigid.velocity.y > 0) // 최고점 도달 시 while문 종료
        {
            yield return null;
        }

        allowCollect = true;
        GetComponent<Collider2D>().isTrigger = false;

        yield return new WaitForSeconds(aliveTImeAfterSpwn);

        Destroy(gameObject);
    }

    // 미리 배치해둔 아이템을 먹을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowCollect && collision.CompareTag("Player"))
        {
            UpdateCollision(collision.transform);
            Managers.Inventory.InventoryItem(itemType, 1);
            Destroy(gameObject);
        }
    }

    // 생성된 아이템 먹을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (allowCollect && collision.transform.CompareTag("Player")) ;
        {
            UpdateCollision(collision.transform);
            Destroy(gameObject);
        }
    }

    // ItemBase를 상속받는 모든 클래스들은 아래 메소드 내용 재정의 필수
    public abstract void UpdateCollision(Transform target);
}
