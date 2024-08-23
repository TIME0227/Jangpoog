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
        Debug.DrawRay(monsterTransform.position,monster.direction.normalized,Color.green);
        
        //타겟이 없어지면 back state로 가야함
        if (monster.target == null)
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFollow", false);
        }
        
        //타겟이 있고, 사정거리보다 먼 경우 타겟을 쫓아가야함
        else if(monster.direction.magnitude>monster.attackRange)
        {
            switch (monster.Detect(monster.direction))
            {
                case -1:
                    monster.movement2D.MoveTo(0);
                    break;
                case 1:
                    if (!monster.movement2D.isJump)
                    {
                        monster.StartCoroutine(monster.CoJump(monster.direction)); 
                    }
                    break;
                case 0:
                    monster.movement2D.MoveTo(monster.direction.normalized.x);
                    break;
            }
        }

        //사정거리 안에 들어오는 경우 ready state로 가야함
        else
        {
            monster.movement2D.MoveTo(0);
            animator.SetBool("isBack",false);
            animator.SetBool("isFollow",false);
        }
        
        //sprite flip
        monster.FlipSprite();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    
    

}
