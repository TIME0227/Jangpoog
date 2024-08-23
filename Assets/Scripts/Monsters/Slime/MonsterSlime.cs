using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlime : Monster
{
    public override void Init()
    {
        base.Init();
        ResetSize();
    }
    private void ResetSize()
    {
        Vector3 currentScale = transform.localScale;
        currentScale *= stat.monsterData.Height / 0.8f;

        transform.localScale = currentScale;
    }

    protected override void OnDie()
    {
        base.OnDie();   
    }
    
    
    
}
