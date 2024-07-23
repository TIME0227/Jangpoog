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

         transform.localScale = currentScale;
     }
     //public 
}
