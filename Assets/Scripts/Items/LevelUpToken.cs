using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpToken : ItemBase
{
    public override void UpdateCollision(Transform target)
    {
        target.GetComponent<PlayerDataManager>().LevelUpToken++;
    }
}
