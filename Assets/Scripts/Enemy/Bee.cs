using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("���ʽd��")]
    public float patrolRadius;
    
    
    protected override void Awake()
    {
        base.Awake();
        //���
        patrolState = new BeePartolState();
        chaseState = new BeeChaseState();
    }

    //�˴��覡��g���j��
    public override bool FoundPlayer() 
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if (obj)
            attacker = obj.transform;
        return obj;
    }

    //�P�W�Aø�s�d�򪺤覡�]��g�A���ʽd��B�j�M�d������ø�s

    public override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    public override Vector3 GetNewPoint()
    {
        //�H����m���g�k�A(�̤p�ȡA�̤j��)
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);

        return spwanPoint + new Vector3(targetX, targetY);
    }

    public override void Move()
    {
        
    }

}
