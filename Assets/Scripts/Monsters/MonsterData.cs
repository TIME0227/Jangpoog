using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data")]
[SerializeField]
public class MonsterData : ScriptableObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string stagePoint;
    [SerializeField] private float height;
    [SerializeField] private float speed;
    
    public float MaxHp => maxHp;
    public float Damage => damage;
    public string StagePoint => stagePoint;
    public float AttackSpeed => attackSpeed;
    //보류
    public float Height => height;
    public float Speed => speed;




}

