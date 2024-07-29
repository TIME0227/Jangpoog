using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	private	Animator animator;
	private	MovementRigidbody2D	movement;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		movement = GetComponentInParent<MovementRigidbody2D>();
	}

	public void UpdateAnimation(float x)
	{
        // 좌/우 방향키 입력이 있을 때
        if ( x != 0 )
		{
            // 플레이어 스프라이트 좌/우 반전 : 바라보는 방향 설정
            SpriteFlipX(x);
		}

		animator.SetBool("isJump", !movement.IsGrounded);

        // 바닥에 닿아 있으면
        if (movement.IsGrounded)
        {
            // velocityX가 0이면 "Idle", velocityX가 0.5이면 "Walk", velocityX가 1이면 "Run" 재생
            animator.SetFloat("velocityX", Mathf.Abs(x));
            animator.SetBool("isLongJump", false);
        }
        // 바닥에 닿아 있지 않으면
        else
        {
            // 더블 점프할 때 - LongJumpUp
            if (movement.IsLongJump == true)
            {
                animator.SetBool("isLongJump", true);
                animator.SetFloat("velocityY", movement.Velocity.y);
            }
            // 더블 점프 끝날 때 - LongJumpDown
            else if (movement.IsLongJump == false)
            {
                animator.SetBool("isLongJump", false);
                animator.SetFloat("velocityY", movement.Velocity.y);

            }
            else
            {
                // velocityY가 음수이면 "JumpDown", velocityY가 양수이면 "JumpUp" 재생
                animator.SetFloat("velocityY", movement.Velocity.y);
            }
        }

    }

    // SpriteRenderer 컴포넌트의 Filp을 이용해 이미지를 반전했을 때
    // 화면에 출력되는 이미지 자체만 반전되기 때문에
    // 플레이어의 전방 특정 위치에서 발사체를 생성하는 것과 같이
    // 방향전환이 필요할 때는 Transform.Scale.x를 -1, 1과 같이 설정
    private void SpriteFlipX(float x)
	{
		transform.parent.localScale = new Vector3((x < 0 ? -1 : 1), 1, 1);
	}

	// player 슬라이딩 애니메이션
	public void StartSliding()
	{
		// Debug.Log("슬라이딩 애니메이션 시작");
        animator.SetBool("isSliding", true);
    }

    public void StopSliding()
    {
        // Debug.Log("슬라이딩 애니메이션 종료");
        animator.SetBool("isSliding", false);
    }

	// player 장풍 애니메이션
	public void JangPoongShooting()
	{
		// Debug.Log("장풍 애니메이션");
		animator.SetTrigger("isShooting");
        animator.SetTrigger("Idle");
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        animator.speed = multiplier;
    }

}


