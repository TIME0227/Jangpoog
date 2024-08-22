using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReadyState : StateMachineBehaviour
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
        monster.Detect(monster.direction);
        //딜레이를 주고, attack trigger 호출
        if (monster.AttackDelay <= 0 && monster.Detect(monster.direction) != -1)
        {
            animator.SetTrigger("Attack");
            
        }
        
        //그리고 플레이어가 attackRange보다 멀어지면 다시 follow 상태로 가게 한다.
        if (monster.direction.magnitude > monster.attackRange)
        {
            animator.SetBool("isFollow", true);
        }
        
        
        monster.FlipSprite();
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    
    
    

}
