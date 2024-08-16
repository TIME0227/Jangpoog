using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTargettingState : StateMachineBehaviour
{
    private Monster monster;
    private Transform monsterTransform;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("타겟팅 애니메이션 호출");
        monster = animator.GetComponentInParent<Monster>();
        monsterTransform = monster.transform;
        

        monster.StartCoroutine(monster.CoTargetting());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    
    

}
