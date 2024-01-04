using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    //[HideInInspector]���ݼ��S�bInspector
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator anim;
    
    [Header("�򥻰Ѽ�")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

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

    [Header("���A")]
    public bool isHurt;
    public bool isDead;


    //��H���A�ƻ�ɭԹ�ҤƳo�Ǫ��A�O?�b�Ǫ��l����(�ҦpBoar)���Ы�
    //��e���A
    protected BaseState currentState;
    //���ު��A
    protected BaseState patrolState;
    //�l�����A
    protected BaseState chaseState;

    #region �g�����
    //��ܳo�Ӥ�k�O�@�ӵ�����k�A�i�H�Q�l���O�мg�C��l���O�мg�o�Ӥ�k�ɡA���i�H���Ѧۤv����{�C
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
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
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        //����BoarPartolState��
        //if ((physicsCheck.touchLeftWall && faceDir.x < 0) || (physicsCheck.touchRightWall && faceDir.x > 0))
        //{
        //    wait = true;
        //    anim.SetBool("walk", false);
        //}

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
    #endregion

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
        
    }

    //�C���n�����A�ɡA�եΦ���k�A�Ӧ�Enums���T�|�ܶq
    public void SwitchState(NPCState state) 
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _=> null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
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

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, 0.2f);
    }

}
