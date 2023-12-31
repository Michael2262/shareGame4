using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自BaseState的抽象方法
public class BoarPartolState : BaseState
{
    public override void OnEnable()
    {
        //發現player切換至chaseState
    }
    public override void LogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit()
    {
        
    }
}
