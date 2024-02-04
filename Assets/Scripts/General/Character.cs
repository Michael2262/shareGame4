using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
//�ե�unity�ƥ�
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("���ݩ�")]
    public float maxHealth;
    public float currentHealth;
    public float currentDefence;
    public float defence = 0f;
    public float specialDefense = 0f;

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
    public UnityEvent<Transform> OnDie;
    public UnityEvent<Character> OnHealthChange;
    //�ĤT�Өƥ�A�n��character���ȶi�h

    private void Start()
    {
        currentHealth = maxHealth;
        currentDefence = defence;
        controlable = true;
        OnHealthChange?.Invoke(this);
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

    //Ĳ��S�wCollider2D�ɱҥ�
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water")) 
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            //"����Ҧ����`���U�ƥ�"
            OnDie?.Invoke(null);
        }

    }

    //����@��attack�������ȶi�ӡA���W�sattacker(������)
    public void TakeDamage(Attack attacker = null) 
    {
        //�Y�K�̪����������
        if (invulnerable || (attacker.damage - currentDefence)<0)
            return;
        
        if(currentHealth - (attacker.damage) > 0) 
        {
            currentHealth -= (attacker.damage);
            TriggerInvulnerable();
            //"����Ҧ����˵��U�ƥ�"(?�קK�S��)�AInvoke�O�ҰʡC���U�ƥ�ɡA�W�w�ݭn�@��transform
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //"����Ҧ����`���U�ƥ�"
            OnDie?.Invoke(attacker.transform) ;
        }

        OnHealthChange?.Invoke(this);
        
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
