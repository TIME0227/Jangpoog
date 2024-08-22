using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBombSlime : MonsterSlime
{
    protected override void OnDie()
    {
        base.OnDie();
        Invoke(nameof(Spawn), 0.9f);
    }

    public void Spawn()
    {
        Debug.Log("ssg 생성");
        GameObject ssg = Managers.Resource.Instantiate("Items/Items_Both/SuperStickyGoo");
        ssg.transform.position = movement2D.FootPosition;
    }
}
