using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //使用Unity事件寫法，在外面用加號把各種方法註冊到此事件中
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
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
    }

    public void TakeDamage(Attack attacker) 
    {
        //若免疫直接停止執行
        if (invulnerable)
            return;
        
        if(currentHealth - attacker.damage > 0) 
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //執行受傷，?是用來如果沒有就跳過，不要報錯。Invoke是啟動
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //觸發死亡
            OnDie?.Invoke() ;
        }

        
    }

    private void TriggerInvulnerable() 
    {
        if (!invulnerable) 
        { 
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
