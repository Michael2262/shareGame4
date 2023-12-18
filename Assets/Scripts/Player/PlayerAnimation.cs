using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation() 
    {
        //Mathf.Abs，unity提供的數學方法，取絕對值
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isCrouch", playerController.isCrouch);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isAttack", playerController.isAttack);
        //anim.SetInteger("combo", playerController.combo);

    }


    public void PlayHurt() 
    {
        //Trigger是單次執行的函數方法，不要放在update裡
        anim.SetTrigger("hurt");
    }

    public void PlayerAttack() 
    {
        anim.SetTrigger("attack");
    }
}
