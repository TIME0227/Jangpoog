using UnityEngine;

public class HPpotionLarge : ItemBase
{
    public override void UpdateCollision(Transform target)
    {
        var playerDataManager = target.GetComponent<PlayerDataManager>();

        if (playerDataManager != null)
        {
            Destroy(gameObject);
        }
    }
}

