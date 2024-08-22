using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBatGoblin : Monster
{
    [SerializeField] private Transform throwStartPosition;
    [SerializeField] private int batVersion = 1;

    public override void Init()
    {
        base.Init();
        stat.DieAction -= ThrowBat;
        stat.DieAction += ThrowBat;

    }

    public void ThrowBat()
    {
        Debug.Log("호출");
        Managers.Resource.Instantiate($"Monsters/Goblin/Goblin_Bat_v{batVersion}", null, throwStartPosition.position);
    }
}
