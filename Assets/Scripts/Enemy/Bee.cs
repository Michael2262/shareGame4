using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("移動範圍")]
    public float patrolRadius;
    
    
    protected override void Awake()
    {
        base.Awake();
        //賦值
        patrolState = new BeePartolState();
        chaseState = new BeeChaseState();
    }

    //檢測方式改寫成大圓
    public override bool FoundPlayer() 
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if (obj)
            attacker = obj.transform;
        return obj;
    }

    //同上，繪製範圍的方式也改寫，移動範圍、搜尋範圍分兩色繪製

    public override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    public override Vector3 GetNewPoint()
    {
        //隨機位置的寫法，(最小值，最大值)
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);

        return spwanPoint + new Vector3(targetX, targetY);
    }

    public override void Move()
    {
        
    }

}
