using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ե�unity�ƥ�
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("���ݩ�")]
    public float maxHealth;
    public float currentHealth;

    [Header("���˵L��")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    [Header("����ʧ@�ɶ�")]
    public float cantControlDuration;
    private float cantControlCounter;
    public bool controlable;

    //�ϥ�Unity�ƥ�g�k�A�b�~���Υ[����U�ؤ�k���U�즹�ƥ�
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        controlable = true;
    }

    //�p�ɾ�������iupdate��
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

    //����@��attack�������ȶi�ӡA���W�sattacker(������)
    public void TakeDamage(Attack attacker) 
    {
        //�Y�K�̪����������
        if (invulnerable)
            return;
        
        if(currentHealth - attacker.damage > 0) 
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //������˵��U�ƥ�(?�קK�S��)�AInvoke�O�ҰʡC���U�ƥ�ɡA�W�w�ݭn�@��transform
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //Ĳ�o���`
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
        if (controlable)
        {
            controlable = false;
            cantControlCounter = cantControlDuration;
        }

    }
}
