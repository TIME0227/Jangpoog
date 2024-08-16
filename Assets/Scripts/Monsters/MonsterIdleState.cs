using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MonsterIdleState : StateMachineBehaviour
{
    private Monster monster;
    private Transform monsterTransform;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponentInParent<Monster>();
        monsterTransform = monster.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster.target != null)
        {
            if (Mathf.Abs(monster.direction.x) <= monster.scanRange)
            {
                animator.SetTrigger("Targetting");
            }
        }
        else
        {
            Debug.Log("타겟 없음");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
