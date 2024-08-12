using UnityEngine;

public class HPpotionLarge : ItemBase
{
    public override void UpdateCollision(Transform target)
    {
        var playerDataManager = target.GetComponent<PlayerDataManager>();

        if (playerDataManager != null)
        {
            // 코루틴을 PlayerDataManager에서 실행
            playerDataManager.StartCoroutine(AddHpAfterDelay(playerDataManager));

            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator AddHpAfterDelay(PlayerDataManager playerDataManager)
    {
        // 포션 마시는 시간
        yield return new WaitForSeconds(3f);

        // HP 증가
        playerDataManager.GetComponent<PlayerDataManager>().Hp += 4;
    }
}

