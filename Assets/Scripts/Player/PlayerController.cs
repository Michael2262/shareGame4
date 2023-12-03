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
    public float speed;

    private void Awake()
    {
        //實例化出來，使用=進行賦值，Awake快於OnEnabl快於star
        inputControl = new PlayerInputControl();

        rb = GetComponent<Rigidbody2D>();
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
        //velocity速度。Time.deltaTime時間修正
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);
    }
}
