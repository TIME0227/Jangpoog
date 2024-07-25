using UnityEngine;
using TMPro;

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
    [SerializeField]
    private GameObject jangPoongPrefab;
    [SerializeField]
    private float jangPoongSpeed = 10.0f;
    [SerializeField]
    private float jangPoongDistance = 5.0f;
    [SerializeField]
    private TextMeshProUGUI manaText;
    [SerializeField]
    private float mana = 100f;
    [SerializeField]
    private float maxMana = 100f;
    [SerializeField]
    private float manaRegenerationRate = 3f;
    [SerializeField]
    private float manaConsumption = 5f;

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

        InvokeRepeating("RegenerateMana", 1f, 1f);  // 1초마다 RegenerateMana 메서드 호출
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
        UpdateManaText();
    }

    // 이동
    private void UpdateMove(float x)
    {
        if (!isSliding)
        {
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
               // capsuleCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
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
            if (mana >= manaConsumption)
            {
                mana -= manaConsumption;

                Vector3 spawnPosition = transform.position;
                spawnPosition.y += isSliding ? 0.2f : 0.7f;

                GameObject jangPoong = Instantiate(jangPoongPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D jangPoongRb = jangPoong.GetComponent<Rigidbody2D>();
                Vector2 jangPoongDirection = new Vector2(transform.localScale.x, 0).normalized;
                jangPoongRb.velocity = jangPoongDirection * jangPoongSpeed;
                playerAnimator.JangPoongShooting();
                Destroy(jangPoong, jangPoongDistance / jangPoongSpeed);
            }
            else // 잔여 마나량 < 5
            {
                Debug.Log("마나량 부족");
            }
        }
    }

    // 마나 재생
    private void RegenerateMana()
    {
        mana = Mathf.Min(mana + manaRegenerationRate, maxMana);
    }

    // 마나 텍스트 업데이트
    private void UpdateManaText()
    {
        manaText.text = $"Mana {mana}/{maxMana}";
    }
}
