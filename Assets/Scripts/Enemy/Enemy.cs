using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    PhysicsCheck physicsCheck;
    //protected�u���l����Ϊ��A���|��public���򤽶}
    protected Animator anim;
    
    [Header("�򥻰Ѽ�")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;


    [Header("�p�ɾ�")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("���A")]
    public bool isHurt;
    public bool isDead;


    //��H��
    //���ު��A
    protected BaseState patroState;
    //��e���A
    protected BaseState currentState;
    //�l�����A
    protected BaseState chaseState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }
    //����Q�E����
    private void OnEnable()
    {
        currentState = patroState;
        currentState.OnEnable();
    }

    private void Start()
    {
        waitTimeCounter = waitTime;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        if ((physicsCheck.touchLeftWall && faceDir.x < 0) || (physicsCheck.touchRightWall && faceDir.x > 0))
        {
            wait = true;
            anim.SetBool("walk", false);
        }
        TimeCounter();

        currentState.LogicUpdate();
    }

    //���z����
    private void FixedUpdate()
    {
        if(!isHurt && !isDead)
            Move();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    //virtual�i�b�l�����A��override�Ƽg
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    //�Ҧ���p�ɾ������A����o��
    public void TimeCounter()
    {
        if (wait) 
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0) 
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }

    public void OnTakeDamage( Transform attackTrans) 
    {
        attacker = attackTrans;
        //�ਭ
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1,1,1);

        //���˳Q���h
        isHurt = true;
        anim.SetTrigger("hurt");
        //Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
            
        //"�Ұʨ�{"���T�w�g�k(���K���{���ܶqdir�Ƕi�h)
        StartCoroutine(KnockBack(attackTrans));
    }

    public void OnDie(Transform attackTrans)
    {

        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;
        StartCoroutine(KnockBack(attackTrans));

    }



    //IEnumerable�A�N��ۤ@�ӥi�H���ǦC���N�����X�C��^�@�Ө�{�C
    //���h�@��
    private IEnumerator KnockBack(Transform attackTrans) 
    {

        Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        //�ϥ�isDead���Ȩ��ˬd���Өϥέ��ӵ��ݮɶ��C
        float waitTime = isDead ? 1.2f : 0.7f;
        yield return new WaitForSeconds(waitTime);
        isHurt = false;
        isDead = false;
    }

    
    //���`�ʵe�����̫�ɽե�
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

}
