using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpToken : ItemBase
{
/*    public override void UpdateCollision(Transform target)
    {
        target.GetComponent<PlayerDataManager>().LevelUpToken++;
    }*/
    public override void UpdateCollision(Transform target)
    {
        var playerDataManager = target.GetComponent<PlayerDataManager>();

        if (playerDataManager != null)
        {
            playerDataManager.LevelUpToken++;

            Destroy(gameObject);
        }
    }
}
