using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeGoblinAttackState : MonsterAttackState
{
    private MonsterWeaponCollider weaponCollider;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        weaponCollider = monster.GetComponentInChildren<MonsterWeaponCollider>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateUpdate(animator, stateInfo, layerIndex);
       
       weaponCollider.AttackPlayerByWeapon();
       
    }
    
    
    

}
