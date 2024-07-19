using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // 점프 & 슬라이딩 데이터 설정
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
    private PlayerDataManager playerDataManager;
    private bool isSliding = false;
    private Vector2 slideDirection;
    private float slideRemainingDistance;
    private Quaternion originalRotation;

    // 더블 클릭 (대쉬) 데이터 설정
    private float doubleClickTimeLimit = 0.25f;
    private float speedMultiplier = 2.0f;
    private bool isDoubleClicking = false;
    private float lastClickTime = -1.0f;
    private KeyCode lastKeyPressed;

    private void Awake()
    {
        movement = GetComponent<MovementRigidbody2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerDataManager = GetComponentInChildren<PlayerDataManager>();
        originalRotation = capsuleCollider.transform.rotation;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;
        x *= offset;

        UpdateMove(x);
        UpdateJump();
        UpdateSlide();
        UpdateJangPoong();
        playerAnimator.UpdateAnimation(x);

        CheckDoubleClick(KeyCode.A);
        CheckDoubleClick(KeyCode.D);
    }

    // 이동
    private void UpdateMove(float x)
    {
        if (!isSliding)
        {
            if (isDoubleClicking) // 대쉬
            {
                x *= speedMultiplier;
                playerAnimator.SetSpeedMultiplier(speedMultiplier);
            }
            else
            {
                playerAnimator.SetSpeedMultiplier(1.0f);
            }
            movement.MoveTo(x);
        }
    }

    // 점프
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

    // 슬라이딩
    private void UpdateSlide()
    {
        if (Input.GetKeyDown(slideKeyCode))
        {
            if (!isSliding)
            {
                isSliding = true;
                slideRemainingDistance = slideDistance;
                slideDirection = new Vector2(transform.localScale.x, 0).normalized;
                capsuleCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
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

            rb.velocity = new Vector2(slideDirection.x * slideSpeed, rb.velocity.y);
            slideRemainingDistance -= moveStep;

            if (slideRemainingDistance <= 0)
            {
                isSliding = false;
                capsuleCollider.transform.rotation = originalRotation;
                playerAnimator.StopSliding();
                rb.velocity = Vector2.zero;
            }
        }
    }

    // 장풍 발사
    private void UpdateJangPoong()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // 장풍 레벨 관련 조건 추가

            if (playerDataManager.mana >= playerDataManager.manaConsumption)
            {
                playerDataManager.mana -= playerDataManager.manaConsumption;

                Vector3 spawnPosition = transform.position;
                spawnPosition.y += isSliding ? 0.2f : 0.7f;

                GameObject jangPoong = Instantiate(playerDataManager.jangPoongPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D jangPoongRb = jangPoong.GetComponent<Rigidbody2D>();
                Vector2 jangPoongDirection = new Vector2(transform.localScale.x, 0).normalized;
                jangPoongRb.velocity = jangPoongDirection * playerDataManager.jangPoongSpeed;
                playerAnimator.JangPoongShooting();
                Destroy(jangPoong, playerDataManager.jangPoongDistance / playerDataManager.jangPoongSpeed);
            }
            else // 잔여 마나량 < 5
            {
                Debug.Log("마나량 부족");
            }
        }
    }

    // 더블 클릭 체크
    private void CheckDoubleClick(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            if (Time.time - lastClickTime < doubleClickTimeLimit && lastKeyPressed == key)
            {
                isDoubleClicking = true;
                StopAllCoroutines();
                StartCoroutine(DoubleClickTimer());
            }
            else
            {
                lastClickTime = Time.time;
                lastKeyPressed = key;
            }
        }

        if (Input.GetKeyUp(key))
        {
            if (isDoubleClicking)
            {
                StopAllCoroutines();
                StartCoroutine(DoubleClickCooldown());
            }
        }
    }

    private IEnumerator DoubleClickTimer()
    {
        while (Input.GetKey(lastKeyPressed))
        {
            yield return null;
        }
        isDoubleClicking = false;
    }

    private IEnumerator DoubleClickCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isDoubleClicking = false;
    }
}
