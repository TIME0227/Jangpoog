using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGoblin : MonsterController
{
    public override void Init()
    {
        base.Init();
        //ResetSize();
    }
    
    // private void ResetSize()
    // {
    //     Vector3 currentScale = transform.localScale;
    //     currentScale *= stat.monsterData.Height / 1.4f;
    //     Debug.Log(stat.monsterData.Height);
    //
    //     transform.localScale = currentScale;
    // }

    protected override void OnDie()
    {
        base.OnDie();
        Debug.Log("고블린 죽음 일반액션");
    }

    protected override void UpdateAttack()
    {
        
    }
}
