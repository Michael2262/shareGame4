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
        //��ҤƥX�ӡA�ϥ�=�i���ȡAAwake�֩�OnEnabl�֩�star
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
    //void�N��S����^�ȡA�N�O�@�Ӱ򥻪����
    public void Move()
    {
        //velocity�t�סCTime.deltaTime�ɶ��ץ�
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);
    }
}
