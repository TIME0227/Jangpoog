using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HPpotionSmall : ItemBase
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