using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    //override重寫父類中有virtual的項目
    //public override void Move()
    //{
    //    base.Move();
    //    anim.SetBool("walk", true);
    //}

    protected override void Awake()
    {
        base.Awake();
        //賦值
        patrolState = new BoarPartolState();
        chaseState = new BoarChaseState();
    }

}
