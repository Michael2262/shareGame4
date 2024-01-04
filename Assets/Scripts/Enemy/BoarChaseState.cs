using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自BaseState的抽象方法
//野豬的追擊狀態

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        Debug.Log("Chase");
    }
    public override void LogicUpdate()
    {
        

    }

    public override void PhysicsUpdate()
    {
       
    }

    public override void OnExit()
    {
        
    }

}
