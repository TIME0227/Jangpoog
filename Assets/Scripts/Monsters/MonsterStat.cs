using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public MonsterData monsterData;
    public float currentHp;
    public float currentDamage;

    public void Init(MonsterData monsterData)
    {
        this.monsterData = ScriptableObject.Instantiate(monsterData);
        currentHp = monsterData.MaxHp;
        currentDamage = monsterData.Damage;

    }
    
}
