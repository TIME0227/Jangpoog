using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Monster : MonoBehaviour
{
    [Header("Stat")] public MonsterStat stat; 
    [Header("Movement")] 
    [SerializeField] private LayerMask obstacleLayer; //장애물로 인식하는 레이어
    public Mon_MovementRigidbody2D movement2D;
    public SpriteRenderer spriteRenderer;
    public Vector3 direction = Vector3.zero;
    public GameObject target;
    
    [Header("Position")]
    protected Vector3 startPos; 
    protected Vector3 destPos;
    
    [Header("Distance/Time Range")]
    [SerializeField] private float moveRange = 2;
    public float scanRange = 8;
    public float attackRange = 1.5f;
    public float minMoveRangeX;
    public float maxMoveRangeX;
    
    [SerializeField] private float waitTime = 1.5f; // 이건 뭘까요
    private float elapsedTime = 0.0f; //
    private float attackDelay = 1.5f; //공격 딜레이 시간
    private float targettingTime = 1.5f; //타겟팅 대기 시간
    
    [Header("Loot")] public List<LootItem> lootTable = new();


    private Animator anim;
    
    private void Start()
    {
        Init(); //초기화
        
        //UI hp bar 설정
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform).gameObject.SetActive(false);
        }
    }
    
    public virtual void Init()
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

        //movement
        movement2D = GetComponent<Mon_MovementRigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        //Postion 설정
        startPos = transform.position;
        minMoveRangeX = startPos.x - moveRange;
        maxMoveRangeX = startPos.x + moveRange;
        
        
        //이벤트 구독
        //stat.DieAction
        
        target = GameObject.FindWithTag("Player");
        
    }

    private void Update()
    {
        CalcDirection();
    }

    private void LateUpdate()
    {
        //FlipSprite();
    }

    #region Control
    // 이동 방향에 따라 이미지 flip
    // spriteRenderer의 flipX를 사용하지 않고 localScale 값 조정으로 구현
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        if (movement2D.Velocity.x > 0)
        {
            scale.x = - Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (movement2D.Velocity.x < 0)
        {
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        
        //transform.localScale = scale;

    }

    void CalcDirection()
    {
        if (target != null)
        {
            direction = target.transform.position - transform.position;
        }
        else
        {
            Debug.Log($"{name} has no target(player)");
        }
    }
    

    #endregion

    #region Coroutine

    public IEnumerator CoTargetting()
    {
        movement2D.MoveTo(0); //움직임 정지 처리
        
        //플레이어(타깃)이 있는 방향으로 방향 전환
        Vector3 scale = transform.localScale;

        if (direction.x > 0)
        {
            Debug.Log("directino is larger than zero");
            scale.x = - Mathf.Abs(scale.x);
        }
        else if (direction.x < 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        
        yield return new WaitForSeconds(1.5f);
        
        anim.SetBool("isFollow", true);
    }
    

    #endregion
}
