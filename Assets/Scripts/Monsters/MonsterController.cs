using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Update = UnityEngine.PlayerLoop.Update;

public abstract class MonsterController : BaseController
{
    [Header("stat")]
    public MonsterStat stat;//private로 수정예정
    
    [Header("Range")]
    [SerializeField] private float moveRange = 2;
    [SerializeField] private float scanRange = 6;
    [SerializeField] protected float attackRange = 1;

    private Vector3 originPos;
    private float minMoveRangeX;
    private float maxMoveRangeX;
    
    [Header("Movement")]
    [SerializeField] private LayerMask obstacleLayer; // 장애물로 인식하는 레이어

    //이동, 움직임 제어
    protected Mon_MovementRigidbody2D movement2D;
    protected new Collider2D collider2D; //충돌
    //protected SpriteRenderer spriteRenderer; //일반, 공격, attack mode
    protected Animator animator; //애니메이션
    
    [SerializeField] private float waitTime = 1.5f; // 대기 시간

    private float elapsedTime = 0.0f;
    
    [SerializeField] protected bool isJumping = false;
    //[SerializeField] private bool isWaiting = true;

    public Vector3 dir = Vector3.zero;

    [Header("Attack")] [SerializeField]
    protected float attackDelay = 3.0f;
    
    

    //private int attackCount=0;
    
    /// <summary>
    /// 초기화 메소드. 스탯 데이터, position, move range 및 변수 초기화
    /// </summary>
    public override void Init()
    {
        stat = gameObject.GetComponent<MonsterStat>();
        MonsterData md = Managers.Resource.Load<MonsterData>($"Data/Monster/{gameObject.name}");
        if (md != null)
        {
            stat.Init(md);
        }
        else
        {
            Debug.Log("Can't Find Monster Stat Data!");

        }

        originPos = transform.position;
        minMoveRangeX = originPos.x - moveRange;
        maxMoveRangeX = originPos.x + moveRange;
        //Debug.Log(minMoveRangeX);
        
        
        movement2D = GetComponent<Mon_MovementRigidbody2D>();
        collider2D = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        //구독
        stat.DieAction -= OnDie;
        stat.DieAction += OnDie;
    }

    private void Start()
    {
        Init();
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform).gameObject.SetActive(false);

        }
    }
    
    #region BaseController Methods
    protected override void OnDie()
    {
        State = Define.State.Die;
        movement2D.MoveTo(0);
        StopAllCoroutines();
        
        Debug.Log("사망");
        //사망 애니메이션 or 사망 상태로 변경
        //아이템 드랍
        Debug.Log("아이템을 드랍합니다.");
       

        //게임 관리 매니져로 수정할 예정
        Destroy(gameObject, 1f); //1초 후에 사라지기
    }
    protected override void UpdateIdle()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        //2초 대기
        elapsedTime += Time.deltaTime;
        if (elapsedTime < waitTime) return;
        
        Debug.Log("기다리기 끝");
        float distance = (player.transform.position - transform.position).magnitude;
        //Debug.Log($"distance:{distance}");
        if (distance <= scanRange)
        {
            if (target == null)
            {
                Debug.Log("플레이어 인식!!!!");
                target = player;
                StartCoroutine(nameof(CoTarget));
                State = Define.State.Target;
            }
            else
            { 
                State = Define.State.Moving;
            }
            
        }
        else
        {
            float randomX = Mathf.Round(Random.Range(minMoveRangeX, maxMoveRangeX) * 10f) / 10f;

            if (Mathf.Abs(randomX - transform.position.x) < 0.5f)
            {
                if (randomX < transform.position.x)
                {
                    randomX = Mathf.Clamp(randomX - 0.5f, minMoveRangeX, maxMoveRangeX);
                }
                else
                {
                    randomX = Mathf.Clamp(randomX + 0.5f, minMoveRangeX, maxMoveRangeX);
                }
            }

            destPos = new Vector3(randomX, transform.position.y, transform.position.z);
            // destPos = new Vector3(Random.Range(minMoveRangeX, maxMoveRangeX), transform.position.y,
            //     transform.position.z);
            State = Define.State.Moving;
            
        }
        elapsedTime = 0.0f;
      
    }

    protected override void UpdateMoving()
    {
        //타겟이 있는 경우
        if (target != null)
        {
            animator.SetBool("isFollow", true);
            //animator.SetBool("isFollow", true);
            destPos = target.transform.position;
            float distance = (destPos - transform.position).magnitude;
            //플레이어가 사정 거리보다 가까우면 공격
            if (distance <= attackRange)
            {
                movement2D.MoveTo(0);
                if (attackDelay >= 0)
                {
                    attackDelay -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool("isFollow", false);
                    State = Define.State.Attack;
                    
                }
                return;

            }
            
        }
       
        //목표 방향으로 가기
        dir = destPos - transform.position; //거리 계산(단, x축만 계산)
        dir.y = 0;
        //목표 위치에 도달시
        if (dir.magnitude < 0.1f)
        {
            Debug.Log("목표 도달 정지");
            movement2D.MoveTo(0);
            State = Define.State.Idle;
        }
        else
        { 
            //움직이기
            Debug.DrawRay(transform.position,dir.normalized,Color.green);
            if (Physics2D.Raycast(transform.position, dir, 1.0f, LayerMask.GetMask("LevelN")))
            {
                Debug.Log("장애물 감지. 더 이상 이동할 수 없음");
                //이동할 수 없는 영역, 장애물을 만남
                State = Define.State.Idle;
                return;
            }
            else if (Physics2D.Raycast(transform.position, dir, 1.0f, LayerMask.GetMask("Level1")))
            {
                //올라갈 수 있는 장애물을 만남
                // State = Define.State.Idle;
                // State = Define.State.Jumping;
                if (!isJumping)
                {
                Debug.Log("점프해서 올라갑니다.");
                    //StartCoroutine(nameof(CoJump));
                    State = Define.State.Jumping;
                }
            }
        
            else
            {
                //이동
                // spriteRenderer.flipX = dir.normalized.x == 1;
                movement2D.MoveTo(dir.normalized.x);
                //animator.SetFloat("velocityX", Mathf.Abs(dir.normalized.x));
            }
        }
    }

    protected override void UpdateJumping()
    {
        if(!isJumping)
        StartCoroutine(nameof(CoJump));
        
    }
    
 #endregion

 
    #region Coroutine

    /// <summary>
    /// 몬스터가 플레이어 인식시 발생하는 모션
    /// </summary>
    /// <returns></returns>
     IEnumerator CoTarget()
     {
         //움직임 정지
         movement2D.MoveTo(0);
         GetComponentInChildren<SpriteRenderer>().flipX = target.transform.position.x >= transform.position.x;
         //잠시 정지
         yield return new WaitForSeconds(1.5f);
        animator.SetBool("isFollow", true);
         
         State = Define.State.Moving;
     }

     /// <summary>
     /// 몬스터가 바닥에 떨어질때까지 대기하다, 떨어지는 순간 움직임 + 물리 + 애니메이션 처리하는 코루틴
     /// </summary>
     /// <returns></returns>
     protected IEnumerator CoJump()
     {
         if (!isJumping)
         {
             isJumping = true;
             movement2D.Jump();
             //animator.SetBool("isJump", true);
             //StartCoroutine(nameof(CoJump));
         }
         yield return new WaitUntil(() => !movement2D.IsGrounded); //점프를 시작시 y 속력이 양수지만 아직 바닥과 붙어있을 수 있음. 바닥과 떨어지는 순간까지 기다림
         while (true)
         {
             //UpdateDirection();
             movement2D.MoveTo(dir.normalized.x);
             //y축 속력에 따라 올라가는 or 내려가는 애니메이션 재생
             //animator.SetFloat("velocityY", movement2D.Velocity.y);
         
             if (movement2D.IsGrounded)
             {
                 Debug.Log("바닥착지");
                 //바닥에 착지시 동작 멈추고 반복문 종료
                 //animator.SetBool("isJump", false);
                 movement2D.MoveTo(0);
                 //animator.SetFloat("velocityX", 0);
                 // //대기 행동 코루틴 호출
                 yield return new WaitForSeconds(2f);
                 isJumping = false;
                 dir = destPos - transform.position; //거리 계산(단, x축만 계산)
                 dir.y = 0;
                 if (State == Define.State.Attack)
                 {
                     attackDelay = 3.0f;
                 }

                 if (dir.magnitude < 0.1f)
                 {
                     Debug.Log("목표 도달 정지");
                     movement2D.MoveTo(0);
                     State = Define.State.Idle;
                 }
                 else
                 {
                     State = Define.State.Moving;
                 }
                
                 yield break;
             }
             yield return null;
         }
     }
     
    
    #endregion
    
    
}
