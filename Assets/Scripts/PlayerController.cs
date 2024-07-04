using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.W;
    [SerializeField]
    private KeyCode slideKeyCode = KeyCode.S;
    [SerializeField]
    private float slideDistance = 3.0f;
    [SerializeField]
    private float slideSpeed = 10.0f;

    private MovementRigidbody2D movement;
    private PlayerAnimator playerAnimator;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rb;
    private bool isSliding = false;
    private Vector2 slideDirection;
    private float slideRemainingDistance;
    private Quaternion originalRotation;

    private void Awake()
    {
        movement = GetComponent<MovementRigidbody2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalRotation = capsuleCollider.transform.rotation;
    }

    private void Update()
    {
        // 키 입력 (좌/우 방향 키, 왼쪽 Shift 키)
        float x = Input.GetAxisRaw("Horizontal");
        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;

        // 걷기일 땐 값의 범위가 -0.5 ~ 0.5
        // 뛰기일 땐 값의 범위가 -1 ~ 1로 설정
        x *= offset;

        // 플레이어의 이동 제어 (좌/우)
        UpdateMove(x);
        // 플레이어의 점프 제어
        UpdateJump();
        // 플레이어의 슬라이드 제어
        UpdateSlide();
        // 플레이어 애니메이션 제어
        playerAnimator.UpdateAnimation(x);
    }

    private void UpdateMove(float x)
    {
        // 슬라이딩 중에는 이동을 막음
        if (!isSliding)
        {
            // 플레이어의 물리적 이동 (좌/우)
            movement.MoveTo(x);
        }
    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKeyCode))
        {
            movement.Jump();
        }

        if (Input.GetKey(jumpKeyCode))
        {
            movement.IsLongJump = true;
        }
        else if (Input.GetKeyUp(jumpKeyCode))
        {
            movement.IsLongJump = false;
        }
    }

    private void UpdateSlide()
    {
        if (Input.GetKeyDown(slideKeyCode))
        {
            if (!isSliding)
            {
                isSliding = true;
                slideRemainingDistance = slideDistance;
                slideDirection = new Vector2(transform.localScale.x, 0).normalized; // 플레이어의 현재 바라보는 방향
                Debug.Log("슬라이딩!");
                // capsuleCollider 회전
                capsuleCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
                // 슬라이딩 애니메이션 시작
                playerAnimator.StartSliding();
            }
        }

        if (isSliding)
        {
            float moveStep = slideSpeed * Time.deltaTime;
            if (moveStep > slideRemainingDistance)
            {
                moveStep = slideRemainingDistance;
            }

            // 슬라이딩 중에는 Rigidbody2D의 속도를 설정해 물리적으로 이동
            rb.velocity = new Vector2(slideDirection.x * slideSpeed, rb.velocity.y);
            slideRemainingDistance -= moveStep;

            if (slideRemainingDistance <= 0)
            {
                isSliding = false;
                Debug.Log("슬라이딩 끝");
                // capsuleCollider 회전 복구
                capsuleCollider.transform.rotation = originalRotation;
                // 슬라이딩 애니메이션 종료
                playerAnimator.StopSliding();
                // 슬라이딩 종료 시 속도 초기화
                rb.velocity = Vector2.zero;
            }
        }
    }
}
