using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�~�Ӧ�BaseState����H��k
//���ު��l�����A

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
