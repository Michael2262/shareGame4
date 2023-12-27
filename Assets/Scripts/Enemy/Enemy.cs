using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    //protected�u���l����Ϊ��A���|��public���򤽶}
    protected Animator anim;
    
    [Header("�򥻰Ѽ�")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }


    private void FixedUpdate()
    {
        Move();
    }

    //virtual�i�b�l�����A��override�Ƽg
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }
}
