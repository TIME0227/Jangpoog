using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBatGoblin : Monster
{
    [SerializeField] private Transform throwStartPosition;
    [SerializeField] private int batVersion = 1;
    
    
    public void ThrowBat()
    {
        Managers.Resource.Instantiate($"Monsters/Goblin/Goblin_Bat_v{batVersion}", null, throwStartPosition.position);
    }
}
