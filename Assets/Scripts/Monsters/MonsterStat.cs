using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public Action DieAction = null;
    
    public MonsterData monsterData;
    public float currentHp;
    public float currentDamage;

    public void Init(MonsterData monsterData)
    {
        this.monsterData = ScriptableObject.Instantiate(monsterData);
        currentHp = monsterData.MaxHp;
        currentDamage = monsterData.Damage;

    }
    
    //플레이어의 장풍 공격력을 가져오는 걸로 수정할 예정
    public void OnAttacked(float damage)
    {
        //움직임 정지
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, rb.velocity.y);
        //HP bar 보이기
        gameObject.GetComponentInChildren<UI_HPBar>(true).ShowHP();
        if (damage < currentHp)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            StartCoroutine(CoChangeColorWithDelay(Color.white, 0.5f));
        } 
        currentHp = Mathf.Clamp(currentHp - damage, 0, currentHp);
        
        if (currentHp == 0)
        {
            DieAction?.Invoke();

        }
    }
    
    /// <summary>
    /// delay 후 오브젝트의 자식 중 SpriteRenderer component 색깔 변화
    /// </summary>
    /// <param name="color">적용할 색상</param>
    /// <param name="delay">초</param>
    /// <returns></returns>
    private IEnumerator CoChangeColorWithDelay(Color color, float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<SpriteRenderer>().color = color;
    }

}
