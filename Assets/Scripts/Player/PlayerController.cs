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

    [Header("基本參數")]
    public float speed;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //實例化出來，使用=進行賦值，Awake快於OnEnabl快於star
        inputControl = new PlayerInputControl();
        //+=註冊一個新函數，待按鍵按下時執行
        inputControl.Gameplay.Jump.started += Jump;
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
    }
    //根據固定時間0.02s刷新，較不理硬體設備，適合物理使用
    private void FixedUpdate()
    {
        Move();
    }
    //void代表沒有返回值，就是一個基本的函數
    public void Move()
    {
        //velocity=速度。Time.deltaTime=時間修正(可以讓不同設備沒有時間差)
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        //臨時變量，藉由(int)將浮點數強制改成int
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        
        //人物翻轉
        transform.localScale = new Vector3(faceDir,1,1);
    }

    //註冊函數有固定寫法，在()內
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP");
        if (physicsCheck.isGround)
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
    }
}
