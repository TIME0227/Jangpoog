using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBackState : StateMachineBehaviour
{
    private Monster monster;
    private Transform monsterTransform;
    private Vector3 dir;
    
    
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
        dir = monster.startPos - monsterTransform.position;
        if (Mathf.Abs(dir.x) < 0.1f || monster.target != null)
        {
            animator.SetBool("isBack", false);
        }
        else
        { 
            if (monster.movement2D.isJump) return;
            switch (monster.Detect(dir))
            {
                case -1:
                    monster.movement2D.MoveTo(0);
                    break;
                case 1:
                    if (!monster.movement2D.isJump)
                    {
                        monster.StartCoroutine(monster.CoJump(dir)); 
                    }
                    break;
                case 0:
                    monster.movement2D.MoveTo(Mathf.Sign(dir.x));
                    break;
            }
            // //초기 위치로 돌아가기
            // monster.movement2D.MoveTo(Mathf.Sign(dir.x));
        }
        
        monster.FlipSprite(dir);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    
    

}
