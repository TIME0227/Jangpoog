using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBatGoblin : Monster
{
    [SerializeField] private Transform throwStartPosition;
    
    public void ThrowBat()
    {
        Managers.Resource.Instantiate("Monsters/Goblin/Goblin_Bat", null, throwStartPosition.position);
    }
}
