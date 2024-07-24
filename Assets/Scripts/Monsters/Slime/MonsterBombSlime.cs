using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBombSlime : MonsterSlime
{
    protected override void OnDie()
    {
        base.OnDie();
        Debug.Log("ssg 생성");
        GameObject ssg = Managers.Resource.Instantiate("Items/Items_Both/SuperStickyGoo");

        ssg.transform.position = transform.position;

    }
    
}
