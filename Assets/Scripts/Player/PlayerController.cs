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
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private Character character;

    [Header("�򥻰Ѽ�")]
    public float speed;
    //=> �Ÿ���ܽb�Y�B��šA�]�Q�٬�Lambda �B��šA�Ω�w�q�@��²�u���ΦW���
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;
    //public int combo;

    private Vector2 originaOffset;
    private Vector2 originbSize;

    [Header("���z����")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("���A")]
    public float crouchJumpForce;
    public bool isCrouch;
    public float hurtForce;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        character = GetComponent<Character>();

        originaOffset = coll.offset;
        originbSize = coll.size;
        

       

        //��ҤƥX�ӡA�ϥ�=�i���ȡAAwake�֩�OnEnabl�֩�star
        inputControl = new PlayerInputControl();
        //���D�A+=���U�@�ӷs��ơA�ݫ�����U�ɰ���
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

        //����
        inputControl.Gameplay.Attack.started += PlayerAttack;
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
        CheckState();

    }
    //�ھکT�w�ɶ�0.02s��s�A�����z�w��]�ơA�A�X���z�ϥ�
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
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
        //�D�n���ʤ�k
        //velocity=�t�סCTime.deltaTime=�ɶ��ץ�(�i�H�����P�]�ƨS���ɶ��t)
        if(!isCrouch || !physicsCheck.isGround)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        //�{���ܶq�A�ǥ�(int)�N�B�I�Ʊj��令int
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        
        //�H��½��
        transform.localScale = new Vector3(faceDir,1,1);

        //�U��

        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch) 
        {
            //�ק�I����j�p
            coll.offset = new Vector2(-0.08f, 0.54f);
            coll.size = new Vector2(0.79f, 1.05f);

        }
        else 
        {
            //��^�I����j�p
            coll.size = originbSize;
            coll.offset = originaOffset;
        }
    }

    //���U��Ʀ��T�w�g�k�A�b()��
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP");
        //if (character.controlable)
        //{
            if (physicsCheck.isGround && !isCrouch)
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            if (physicsCheck.isGround && isCrouch)
                rb.AddForce(transform.up * crouchJumpForce, ForceMode2D.Impulse);
        //}
        
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {


        //return����������
        

        if (physicsCheck.isGround && character.controlable) 
        {
            playerAnimation.PlayerAttack();
            isAttack = true;
        }
            
        //combo++;
        //if (combo >= 3)
        //    combo = 0;
    }

    private void CheckState() 
    {
        //�T���B��šA�ھ�physicsCheck.isGround���ȬO�P�_�A�N�I���������z���Ƴ]�m�� normal �� wall
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }


    #region UnityEvent

    public void GetHurt(Transform attacker) 
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        //.normalized"�k�@��"��ܤ�V�Ӥ����ߨ��骺�j�p
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead() 
    {
        isDead = true;
        inputControl.Gameplay.Disable();


    }

    #endregion
}
