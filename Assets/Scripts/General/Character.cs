using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//調用unity事件
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本屬性")]
    public float maxHealth;
    public float currentHealth;

    [Header("受傷無敵")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    [Header("不能動作時間")]
    public float cantControlDuration;
    private float cantControlCounter;
    public bool controlable;

    //使用Unity事件寫法，在外面用加號把各種方法註冊到此事件中
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent<Transform> OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        controlable = true;
    }

    //計時器相關放進update中
    private void Update()
    {
        if (invulnerable) 
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0) 
            {
                invulnerable = false;
            }

        }

        if (!controlable)
        {
            cantControlCounter -= Time.deltaTime;
            if (cantControlCounter <= 0)
            {
                controlable = true;
            }

        }

    }

    //收到一個attack類型的值進來，取名叫attacker(攻擊者)
    public void TakeDamage(Attack attacker = null) 
    {
        //若免疫直接停止執行
        if (invulnerable)
            return;
        
        if(currentHealth - attacker.damage > 0) 
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //執行受傷註冊事件(?避免沒有)，Invoke是啟動。註冊事件時，規定需要一個transform
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //觸發死亡
            OnDie?.Invoke(attacker.transform) ;
        }

        
    }

    private void TriggerInvulnerable() 
    {
        if (!invulnerable) 
        { 
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
        if (controlable)
        {
            controlable = false;
            cantControlCounter = cantControlDuration;
        }

    }
}
