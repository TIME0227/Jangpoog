using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlime : MonsterBase
{
    [SerializeField] private LayerMask obstacleLayer; // 장애물로 인식하는 레이어

    //이동, 움직임 제어
    private MovementRigidbody2D movement2D;
    private Collider2D collider2D; //충돌
    private SpriteRenderer spriteRenderer; //일반, 공격, attack mode
    private Animator animator; //애니메이션


    [SerializeField] private float waitTime = 2f; // 대기 시간
    [SerializeField] private int moveDirection = 0; // 이동 방향(x)
    [SerializeField] private float moveTime_min = 1f; //이동 시간(최소)
    [SerializeField] private float moveTime_max = 2f; //이동 시간(최대)

    [SerializeField] private bool isJumping = false;


    private void Awake()
    {
        movement2D = GetComponent<MovementRigidbody2D>();
        collider2D = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(nameof(Idle));
    }

    /// <summary>
    /// 대기 행동
    /// </summary>
    /// <returns></returns> 
    private IEnumerator Idle()
    {
        //waitTime 동안 대기
        Debug.Log("대기 중");
        yield return new WaitForSeconds(waitTime);

        //랜덤으로 다음 행동을 결정한다.
        moveDirection = Random.Range(-1, 2); // 1:오른쪽, 0:정지, -1:왼쪽
        if (moveDirection != 0)
        {
            spriteRenderer.flipX = moveDirection == -1; //정지 상태일 때는 변화를 줄 필요가 없음!
            StartCoroutine(nameof(Walk));
        }
        else
        {
            StartCoroutine(nameof(Idle));
        }

        yield return null;

    }

    //이동
    private IEnumerator Walk()
    {
        Debug.Log("걷는 중");
        float time = 0;
        float moveTime = Random.Range(moveTime_min, moveTime_max);
        Debug.Log(moveTime);

        animator.SetFloat("velocityX", Mathf.Abs(moveDirection));
        movement2D.MoveTo(moveDirection);

        while (time < moveTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        movement2D.MoveTo(0);
        //animator.SetBool("isWalk", false);
        animator.SetFloat("velocityX", 0);
        StartCoroutine(nameof(Idle));

    }

    private void FixedUpdate()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Bounds bounds = collider2D.bounds; //몬스터의 collider 범위 가져오기
        //전방에 가로- 0.1, 세로- 몬스터 높이*0.8의 충돌박스 생성
        Vector2 size = new Vector2((bounds.max.x - bounds.min.x) * 1.5f, (bounds.max.y - bounds.min.y) * 0.8f); //사이즈
        Vector2 position = new Vector2(moveDirection == -1 ? bounds.min.x : bounds.max.x, bounds.center.y); //위치
        //Physics2D.OverlapBox로 장애물 감지시 방향 전환
        if (Physics2D.OverlapBox(position, size, 0, obstacleLayer))
        {
            if (movement2D.IsGrounded && !isJumping)
            {
                if (movement2D.HitBelowObject.gameObject.layer == 7)
                {
                    Debug.Log("장애물 감지! 방향 전환");
                    moveDirection *= -1;
                    movement2D.MoveTo(moveDirection);
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
                else
                {
                    Debug.Log("장애물 감지! 점프");
                    StartCoroutine(nameof(Jump));

                }
            }
        }
    }

    //점프
    private IEnumerator Jump()
    {
        isJumping = true;
        movement2D.Jump();
        animator.SetBool("isJump", true);
        yield return new WaitUntil(() => !movement2D.IsGrounded); //점프를 시작시 y 속력이 양수지만 아직 바닥과 붙어있을 수 있음. 바닥과 떨어지는 순간까지 기다림
        while (true)
        {
            //UpdateDirection();
            movement2D.MoveTo(moveDirection);
            //y축 속력에 따라 올라가는 or 내려가는 애니메이션 재생
            animator.SetFloat("velocityY", movement2D.Velocity.y);

            if (movement2D.IsGrounded)
            {
                Debug.Log("바닥착지");
                //바닥에 착지시 동작 멈추고 반복문 종료
                animator.SetBool("isJump", false);
                movement2D.MoveTo(0);
                animator.SetFloat("velocityX", 0);
                isJumping = false;
                //대기 행동 코루틴 호출
                yield return new WaitForSeconds(1f);
                StartCoroutine(nameof(Idle));
                yield break;

            }
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        yield return null;
    }

    public override void OnDie()
    {

    }


}
