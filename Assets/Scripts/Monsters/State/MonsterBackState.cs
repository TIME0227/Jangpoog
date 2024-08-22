using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBackState : StateMachineBehaviour
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
        //몬스터가 초기 위치에 거의 접근한 경우
        //혹은, 돌아가던 중에 플레이어를 다시 인식한 경우
        //idle 상태로 다시 빠져나가도록 한다.
        if (Vector2.Distance(monster.startPos, monsterTransform.position) < 0.1f || monster.target != null)
        {
            animator.SetBool("isBack", false);
        }

        else
        {
            //초기 위치로 돌아가기
            int dir = (monster.startPos.x - monsterTransform.position.x) > 0 ? 1 : -1;
            monster.movement2D.MoveTo(dir);
        }
        
        monster.FlipSprite();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    
    

}
