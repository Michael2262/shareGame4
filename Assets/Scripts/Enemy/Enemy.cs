﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//一定要有的組件，沒有的會自動添加
[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]


public class Enemy : MonoBehaviour
{
    //[HideInInspector]不需暴露在Inspector
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator anim;
    
    [Header("基本參數")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    public bool ImageDirRight;
    public Vector3 faceDir;
    public float hurtForce;
    //起始位置，會在awake被記錄
    public Vector3 spwanPoint;

    public Transform attacker;

    [Header("檢測")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("計時器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("狀態")]
    public bool isHurt;
    public bool isDead;


    //抽象類，甚麼時候實例化這些狀態呢?在怪物子集中(例如Boar)中創建(賦值)
    //當前狀態
    protected BaseState currentState;
    //巡邏狀態
    protected BaseState patrolState;
    //追擊狀態
    protected BaseState chaseState;
    //特殊技能狀態
    protected BaseState skillState;

    #region 週期函數
    //表示這個方法是一個虛擬方法，可以被子類別覆寫。當子類別覆寫這個方法時，它可以提供自己的實現。
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        spwanPoint = transform.position;
    }
    //物體被激活時
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
        //呼叫當前"狀態"的 OnEnable 方法，並將當前物件的引用 this 傳遞給該方法。(狀態機)
    }

    private void Start()
    {
        waitTimeCounter = waitTime;
    }

    protected virtual void Update()
    {
        
        if(ImageDirRight)
            faceDir = new Vector3(transform.localScale.x, 0, 0);
        else
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    //物理相關
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

    #region 函數方法

    //virtual可在子類中，用override複寫
    public virtual void Move()
    {
        //一個蝸牛的額外條件判斷，如果動畫沒有撥第0層的"snailPreMove"，才會動作
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("snailPreMove") && !anim.GetCurrentAnimatorStateInfo(0).IsName("snailRecover"))
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
        if (!FoundPlayer() && lostTimeCounter > 0)
            lostTimeCounter -= Time.deltaTime;
        else
            lostTimeCounter = lostTime;
    }
    #endregion

    public virtual bool FoundPlayer()
    {
        //var Temp =  Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
        //這樣可以檢測到底是甚麼類型的值，上面是 RayCastHit的值，但任何值都可以是bool
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
        
    }

    //每次要換狀態時，調用此方法，來自Enums的枚舉變量
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



    #region Unity事件執行方法
    public void OnTakeDamage( Transform attackTrans) 
    {
        //attacker = attackTrans;
        //轉身
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1,1,1);

        //受傷被擊退
        isHurt = true;
        anim.SetTrigger("hurt");
        //停止繼續前進
        rb.velocity = new Vector2(0, rb.velocity.y);    
        //"啟動協程"的固定寫法(順便把臨時變量dir傳進去)
        StartCoroutine(KnockBack(attackTrans));
    }

    public void OnDie(Transform attackTrans)
    {

        
        
        gameObject.layer = 2;
        isDead = true;
        anim.SetBool("dead", true);
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(KnockBack(attackTrans));

    }



    //IEnumerable，代表著一個可以按序列迭代的集合。返回一個協程。
    //擊退作用
    private IEnumerator KnockBack(Transform attackTrans) 
    {
        
        //.normalized：这个方法用于将得到的向量进行标准化，即将向量的长度归一化为 1，保持方向不变。
        Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        //使用isDead的值來檢查應該使用哪個等待時間。
        float waitTime = isDead ? 1.2f : 0.6f;
        yield return new WaitForSeconds(waitTime);
        isHurt = false;
        //isDead = false;
    }

    
    //死亡動畫撥放到最後時調用
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
