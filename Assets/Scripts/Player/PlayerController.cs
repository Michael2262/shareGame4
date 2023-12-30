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

    [Header("基本參數")]
    public float speed;
    //=> 符號表示箭頭運算符，也被稱為Lambda 運算符，用於定義一個簡短的匿名函數
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;
    //public int combo;

    private Vector2 originaOffset;
    private Vector2 originbSize;

    [Header("物理材質")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("狀態")]
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
        

       

        //實例化出來，使用=進行賦值，Awake快於OnEnabl快於star
        inputControl = new PlayerInputControl();
        //跳躍，+=註冊一個新函數，待按鍵按下時執行
        inputControl.Gameplay.Jump.started += Jump;

        #region 強制走路
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

        //攻擊
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

    //每一偵都會執行
    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        CheckState();

    }
    //根據固定時間0.02s刷新，較不理硬體設備，適合物理使用
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
            Move();
    }

    //測試OnTrigger看碰撞器有無交叉
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //}

    //void代表沒有返回值，就是一個基本的函數
    public void Move()
    {
        //主要移動方法
        //velocity=速度。Time.deltaTime=時間修正(可以讓不同設備沒有時間差)
        if(!isCrouch || !physicsCheck.isGround)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        //臨時變量，藉由(int)將浮點數強制改成int
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        
        //人物翻轉
        transform.localScale = new Vector3(faceDir,1,1);

        //下蹲

        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch) 
        {
            //修改碰撞體大小
            coll.offset = new Vector2(-0.08f, 0.54f);
            coll.size = new Vector2(0.79f, 1.05f);

        }
        else 
        {
            //返回碰撞體大小
            coll.size = originbSize;
            coll.offset = originaOffset;
        }
    }

    //註冊函數有固定寫法，在()內
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


        //return不執行後方函數
        

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
        //三元運算符，根據physicsCheck.isGround的值是與否，將碰撞器的物理材料設置為 normal 或 wall
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }


    #region UnityEvent

    public void GetHurt(Transform attacker) 
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        //.normalized"歸一化"表示方向而不關心具體的大小
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
