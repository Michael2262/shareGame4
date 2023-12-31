using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    PhysicsCheck physicsCheck;
    //protected只有子類能用的，不會像public那麼公開
    protected Animator anim;
    
    [Header("基本參數")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;


    [Header("計時器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("狀態")]
    public bool isHurt;
    public bool isDead;


    //抽象類
    //巡邏狀態
    protected BaseState patroState;
    //當前狀態
    protected BaseState currentState;
    //追擊狀態
    protected BaseState chaseState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }
    //物體被激活時
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

    //物理相關
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

    //virtual可在子類中，用override複寫
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    //所有跟計時器相關，都放這兒
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
        //轉身
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1,1,1);

        //受傷被擊退
        isHurt = true;
        anim.SetTrigger("hurt");
        //Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
            
        //"啟動協程"的固定寫法(順便把臨時變量dir傳進去)
        StartCoroutine(KnockBack(attackTrans));
    }

    public void OnDie(Transform attackTrans)
    {

        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;
        StartCoroutine(KnockBack(attackTrans));

    }



    //IEnumerable，代表著一個可以按序列迭代的集合。返回一個協程。
    //擊退作用
    private IEnumerator KnockBack(Transform attackTrans) 
    {

        Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        //使用isDead的值來檢查應該使用哪個等待時間。
        float waitTime = isDead ? 1.2f : 0.7f;
        yield return new WaitForSeconds(waitTime);
        isHurt = false;
        isDead = false;
    }

    
    //死亡動畫撥放到最後時調用
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

}
