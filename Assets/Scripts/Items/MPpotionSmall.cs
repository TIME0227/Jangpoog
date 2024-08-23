using UnityEngine;

public class MPpotionSmall : ItemBase
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
