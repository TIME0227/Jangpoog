using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnForce = new Vector2(1, 7); // ������ ���� �� �̵� ��
    [SerializeField]
    private float aliveTImeAfterSpwn = 5;                   // ������ ���� �� ���� �ð�

    private bool allowCollect = true; // ������ ȹ�� ���� ����

    public Define.Item itemType; // ������ Ÿ��

    public void Setup()                                                    
    {
        // ������ ���� �ڷ�ƾ �޼ҵ� ȣ��
        StartCoroutine(nameof(SpawnItemProcess));
    }

    // ������ ����
    private IEnumerator SpawnItemProcess()
    {
        allowCollect = false;

        var rigid = gameObject.AddComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.velocity = new Vector2(Random.Range(-spawnForce.x, spawnForce.x), spawnForce.y);

        while (rigid.velocity.y > 0) // �ְ��� ���� �� while�� ����
        {
            yield return null;
        }

        allowCollect = true;
        GetComponent<Collider2D>().isTrigger = false;

        yield return new WaitForSeconds(aliveTImeAfterSpwn);

        Destroy(gameObject);
    }

    // �̸� ��ġ�ص� �������� ���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowCollect && collision.CompareTag("Player"))
        {
            UpdateCollision(collision.transform);
            Managers.Inventory.InventoryItem(itemType, 1);
            Destroy(gameObject);
        }
    }

    // ������ ������ ���� ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (allowCollect && collision.transform.CompareTag("Player")) ;
        {
            UpdateCollision(collision.transform);
            Destroy(gameObject);
        }
    }

    // ItemBase�� ��ӹ޴� ��� Ŭ�������� �Ʒ� �޼ҵ� ���� ������ �ʼ�
    public abstract void UpdateCollision(Transform target);
}
