using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation() 
    {
        //Mathf.Abs，unity提供的數學方法，取絕對值
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
    }

}
