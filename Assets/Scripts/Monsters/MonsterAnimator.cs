using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    protected Animator anim;
    protected Mon_MovementRigidbody2D movement;
    protected SpriteRenderer spriteRenderer;
    protected MonsterController monsterController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Mon_MovementRigidbody2D>();
        monsterController = GetComponentInParent<MonsterController>();

    }

    public void UpdateAnimation()
    {
        if (movement.Velocity.x != 0)
        {
            spriteRenderer.flipX = monsterController.dir.normalized.x == 1;
        }
        
        anim.SetBool("isJump", !movement.IsGrounded);

        if (movement.IsGrounded)
        {
            anim.SetFloat("velocityX", movement.Velocity.x>0?1:0);
        }
        else
        {
            anim.SetFloat("velocityY", movement.Velocity.y);
        }
        // if(movement.Velocity.y!=0)
        // {
        //     anim.SetFloat("velocityY", movement.Velocity.y);
        // }
        
    }
    
    
    public void OnHitEvent(){}

    
}
