using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sliding : MonoBehaviour
{
    DoubleJump dj;
    Rigidbody2D rb;
    BoxCollider2D BoxCollider2D;
    private bool isSliding = false;    // 슬라이딩 중이면 true
    private Vector2 slideDirection;    // 슬라이딩 방향
    private float slideRemainingDistance;  // 남은 슬라이딩 거리
    private Vector2 originalColliderSize;  // 기존 Player Collider Size
    private Vector2 originalColliderOffset;  // 기존 Player Collider Offset

    [SerializeField] private float slideDistance = 3.0f;  // 슬라이딩 거리
    [SerializeField] private LayerMask groundLayer;  // Ground 레이어 설정

    [NonSerialized] public float slideSpeed = 5.0f;  // 슬라이딩 속도

    void Start()
    {
        dj = GetComponent<DoubleJump>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalColliderSize = BoxCollider2D.size;
        originalColliderOffset = BoxCollider2D.offset;
    }

    void Update()
    {
        UpdateSlide();
    }

    #region 슬라이딩
    private void UpdateSlide()
    {
        if (dj.isGround)
        {
            if (Input.GetKeyDown(Managers.KeyBind.slideKeyCode))  // 슬라이딩 키 눌렀을 때
            {
                if (!isSliding)
                {
                    StartSlide();
                }
            }
        }

        if (isSliding)
        {
            HandleSliding();
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideRemainingDistance = slideDistance;
        slideDirection = new Vector2(transform.localScale.x, 0).normalized;

        // 콜라이더 크기와 위치 조정 (플레이어를 작게)
        BoxCollider2D.size = new Vector2(9f, 4.0f);
        BoxCollider2D.offset = new Vector2(BoxCollider2D.offset.x, BoxCollider2D.offset.y - 3.0f);

        Debug.Log("슬라이딩 시작");
    }

    private void HandleSliding()
    {
        // 머리 위에 Ground 레이어 유무 체크
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.0f, groundLayer);
        if (hit.collider != null)
        {
            Debug.Log("머리 위에 장애물 존재");
            // 머리 위에 장애물이 있는 동안 슬라이딩 상태 유지
            rb.velocity = new Vector2(slideDirection.x * slideSpeed, rb.velocity.y);
            return;
        }

        float moveStep = slideSpeed * Time.deltaTime;
        if (moveStep > slideRemainingDistance)
        {
            moveStep = slideRemainingDistance;
        }

        rb.velocity = new Vector2(slideDirection.x * slideSpeed, rb.velocity.y);
        slideRemainingDistance -= moveStep;

        // 슬라이딩 종료 조건
        if (slideRemainingDistance <= 0)
        {
            EndSlide();
        }
    }

    private void EndSlide()
    {
        isSliding = false;
        // 콜라이더 크기 및 위치를 원래대로 복구
        BoxCollider2D.size = originalColliderSize;
        BoxCollider2D.offset = originalColliderOffset;
        rb.velocity = Vector2.zero;  // 속도 초기화

        Debug.Log("슬라이딩 종료");
    }
    #endregion
}
