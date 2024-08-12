using UnityEngine;

public class MPpotionLarge : ItemBase
{
    public override void UpdateCollision(Transform target)
    {
        var playerDataManager = target.GetComponent<PlayerDataManager>();

        if (playerDataManager != null)
        {
            // 코루틴을 PlayerDataManager에서 실행
            playerDataManager.StartCoroutine(AddManaAfterDelay(playerDataManager));

            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator AddManaAfterDelay(PlayerDataManager playerDataManager)
    {
        // 포션 마시는 시간
        yield return new WaitForSeconds(2f);

        // Mana 증가
        playerDataManager.GetComponent<PlayerDataManager>().Mana += 50;
    }
}