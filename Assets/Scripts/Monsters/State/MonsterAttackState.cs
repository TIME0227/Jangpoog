using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : StateMachineBehaviour
{
    protected Monster monster;
    protected Transform monsterTransform;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponentInParent<Monster>();
        monsterTransform = monster.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!monster.movement2D.isJump)
        {
            monster.FlipSprite();
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.AttackDelay = monster.attackCoolTime;
    }
    
    
    

}
