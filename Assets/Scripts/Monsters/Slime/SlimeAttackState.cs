using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : MonsterAttackState
{
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        
        if (!monster.movement2D.isJump)
        {
            monster.StartCoroutine(monster.CoJump(monster.direction)); 
        }
    }
    

}
