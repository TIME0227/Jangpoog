using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlime : MonsterController
{

//
     public override void Init()
     {
         base.Init();
         WorldObjectType = Define.WorldObject.Slime;
         ResetSize();
     }
//
     private void ResetSize()
     {
         Vector3 currentScale = transform.localScale;
         currentScale *= stat.monsterData.Height / 0.8f;
         Debug.Log(stat.monsterData.Height);

         transform.localScale = currentScale;
     }

     protected override void OnDie()
     {
         base.OnDie();
         Debug.Log("슬라임 죽음 일반액션");
     }
}
