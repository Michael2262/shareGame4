using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本屬性")]
    public float maxHealth;
    public float currentHealth;

    [Header("受傷無敵")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

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
        }
        else 
        {
            currentHealth = 0;
            //觸發死亡
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
