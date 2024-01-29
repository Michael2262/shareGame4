using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//�@�w�n�����ե�A�S�����|�۰ʲK�[
[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]


public class Enemy : MonoBehaviour
{
    //[HideInInspector]���ݼ��S�bInspector
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator anim;
    
    [Header("�򥻰Ѽ�")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    public bool ImageDirRight;
    public Vector3 faceDir;
    public float hurtForce;
    //�_�l��m�A�|�bawake�Q�O��
    public Vector3 spwanPoint;

    public Transform attacker;

    [Header("�˴�")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("�p�ɾ�")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("���A")]
    public bool isHurt;
    public bool isDead;


    //��H���A�ƻ�ɭԹ�ҤƳo�Ǫ��A�O?�b�Ǫ��l����(�ҦpBoar)���Ы�(���)
    //��e���A
    protected BaseState currentState;
    //���ު��A
    protected BaseState patrolState;
    //�l�����A
    protected BaseState chaseState;
    //�S��ޯબ�A
    protected BaseState skillState;

    #region �g�����
    //��ܳo�Ӥ�k�O�@�ӵ�����k�A�i�H�Q�l���O�мg�C��l���O�мg�o�Ӥ�k�ɡA���i�H���Ѧۤv����{�C
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        spwanPoint = transform.position;
    }
    //����Q�E����
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
        //�I�s��e"���A"�� OnEnable ��k�A�ñN��e���󪺤ޥ� this �ǻ����Ӥ�k�C(���A��)
    }

    private void Start()
    {
        waitTimeCounter = waitTime;
    }

    private void Update()
    {
        
        if(ImageDirRight)
            faceDir = new Vector3(transform.localScale.x, 0, 0);
        else
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    //���z����
    private void FixedUpdate()
    {
        if(!isHurt && !isDead && !wait)
            Move();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    #endregion

    #region ��Ƥ�k

    //virtual�i�b�l�����A��override�Ƽg
    public virtual void Move()
    {
        //�@�ӽ������B�~����P�_�A�p�G�ʵe�S������0�h��"snailPreMove"�A�~�|�ʧ@
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("snailPreMove") && !anim.GetCurrentAnimatorStateInfo(0).IsName("snailRecover"))
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
        if (!FoundPlayer() && lostTimeCounter > 0)
            lostTimeCounter -= Time.deltaTime;
        else
            lostTimeCounter = lostTime;
    }
    #endregion

    public virtual bool FoundPlayer()
    {
        //var Temp =  Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
        //�o�˥i�H�˴��쩳�O�ƻ��������ȡA�W���O RayCastHit���ȡA������ȳ��i�H�Obool
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
        
    }

    //�C���n�����A�ɡA�եΦ���k�A�Ӧ�Enums���T�|�ܶq
    public void SwitchState(NPCState state) 
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Skill => skillState,
            _=> null
        };

        //if (currentState != null)
        //{
            
            currentState.OnExit();
        //}

        currentState = newState;

        //if (currentState != null)
        //{
            
            currentState.OnEnter(this);
        //}
    }

    public virtual Vector3 GetNewPoint() 
    {
        return transform.position;
    }



    #region Unity�ƥ�����k
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
        Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);    
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

    #endregion

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+ new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }

}
