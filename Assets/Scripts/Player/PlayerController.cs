using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    [Header("�򥻰Ѽ�")]
    public float speed;
    //=> �Ÿ���ܽb�Y�B��šA�]�Q�٬�Lambda �B��šA�Ω�w�q�@��²�u���ΦW���
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;

    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //��ҤƥX�ӡA�ϥ�=�i���ȡAAwake�֩�OnEnabl�֩�star
        inputControl = new PlayerInputControl();
        //+=���U�@�ӷs��ơA�ݫ�����U�ɰ���
        inputControl.Gameplay.Jump.started += Jump;

        #region �j���
        runSpeed = speed;

        inputControl.Gameplay.WalkButton.performed += ctx =>
        {
            if (physicsCheck.isGround)
                speed = walkSpeed;
        };
        inputControl.Gameplay.WalkButton.canceled += ctx =>
        {
            if (physicsCheck.isGround)
                speed = runSpeed;
        };
        #endregion 
    }



    private void OnEnable()
    {
        inputControl.Enable();

        
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    //�C�@�����|����
    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }
    //�ھکT�w�ɶ�0.02s��s�A�����z�w��]�ơA�A�X���z�ϥ�
    private void FixedUpdate()
    {
        Move();
    }

    //����OnTrigger�ݸI�������L��e
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //}

    //void�N��S����^�ȡA�N�O�@�Ӱ򥻪����
    public void Move()
    {
        //velocity=�t�סCTime.deltaTime=�ɶ��ץ�(�i�H�����P�]�ƨS���ɶ��t)
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        //�{���ܶq�A�ǥ�(int)�N�B�I�Ʊj��令int
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        
        //�H��½��
        transform.localScale = new Vector3(faceDir,1,1);
    }

    //���U��Ʀ��T�w�g�k�A�b()��
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP");
        if (physicsCheck.isGround)
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
    }
}
