using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.W;


    private MovementRigidbody2D movement;
    private PlayerAnimator playerAnimator;


    private void Awake()
    {
        movement = GetComponent<MovementRigidbody2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    private void Update()
    {
        // 키 입력 (좌/우 방향 키, 왼쪽 Shift 키)
        float x = Input.GetAxisRaw("Horizontal");
        //float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f; //도현 : 뛰기 없으므로 주석처리(0704)

        // 걷기일 땐 값의 범위가 -0.5 ~ 0.5
        // 뛰기일 땐 값의 범위가 -1 ~ 1로 설정
        //x *= offset;

        // 플레이어의 이동 제어 (좌/우)
        UpdateMove(x);
        // 플레이어의 점프 제어
        UpdateJump();
        // 플레이어 애니메이션 제어
        playerAnimator.UpdateAnimation(x);
        /*        // 머리/발에 충돌한 오브젝트 처리
                UpdateAboveCollision();
                UpdateBelowCollision();
                // 원거리 공격 제어
                UpdateRangeAttack();
                // 플레이어가 낭떠러지로 추락했는지 검사
                IsUnderGround();*/
    }

    private void UpdateMove(float x)
    {
        // 플레이어의 물리적 이동 (좌/우)
        movement.MoveTo(x);

        /*        // 플레이어의 x축 이동 한계치 설정 (PlayerLimitMinX ~ PlayerLimitMaxX)
                float xPosition = Mathf.Clamp(transform.position.x, stageData.PlayerLimitMinX, stageData.PlayerLimitMaxX);
                transform.position = new Vector2(xPosition, transform.position.y);*/
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

    /*    private void UpdateAboveCollision()
        {
            if (movement.Velocity.y >= 0 && movement.HitAboveObject != null)
            {
                // 플레이어의 머리와 오브젝트가 충돌했기 때문에 y축 속력을 0으로 설정
                movement.ResetVelocityY();

                // 플레이어의 머리와 충돌한 오브젝트가 Tile일 때 Tile의 속성에 따라 충돌 처리
                if (movement.HitAboveObject.TryGetComponent<TileBase>(out var tile) && !tile.IsHit)
                {
                    tile.UpdateCollision();
                }
            }
        }*/

    /* private void UpdateBelowCollision()
     {
         if (movement.HitBelowObject != null)
         {
             // Platform_03_Oneway
             if (Input.GetKeyDown(KeyCode.DownArrow) && movement.HitBelowObject.TryGetComponent<PlatformEffectorExtension>(out var p))
             {
                 p.OnDownWay();
             }

             if (movement.HitBelowObject.TryGetComponent<PlatformBase>(out var platform))
             {
                 platform.UpdateCollision(gameObject);
             }
         }
     }*/

    /*    private void UpdateRangeAttack()
        {
            if (Input.GetKeyDown(fireKeyCode) && playerData.CurrentProjectile > 0)
            {
                playerData.CurrentProjectile--;

                weapon.StartFire(lastDirectionX);
            }
        }*/

    /*    private void IsUnderGround()
        {
            if (transform.position.y < stageData.MapLimitMinY)
            {
                OnDie();
            }
        }*/

    /*    public void OnDie()
        {
            gameController.LevelFailed();
        }*/

    /*    public void LevelComplete()
        {
            gameController.LevelComplete();
        }*/
}

