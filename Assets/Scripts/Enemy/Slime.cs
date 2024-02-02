using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    
    
    protected override void Awake()
    {
        base.Awake();
        //½á­È
        patrolState = new SlimePartolState();
        chaseState = new SlimeChaseState();
    }

    protected override void Update()
    {
        base.Update();
        if(!physicsCheck.isGround)
            anim.SetBool("isGround", false);
        else
            anim.SetBool("isGround", true);
    }
}
