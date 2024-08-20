using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollowState : StateMachineBehaviour
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
        if(monster)
        monsterTransform.position = Vector2.MoveTowards(monsterTransform.position, monster.target.transform.position,
            Time.deltaTime * monster.GetComponent<Mon_MovementRigidbody2D>().WalkSpeed);

        // //사정 거리보다 가까우면 공격
        // if (monster.direction.x >= -monster.attackRange && monster.direction.x <= monster.attackRange)
        // {
        //     
        // }
        // else
        // {
        //     
        // }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    
    

}
