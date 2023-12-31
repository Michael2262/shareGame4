using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自BaseState的抽象方法
public class BoarPartolState : BaseState
{
    public override void OnEnable(Enemy enemy)
    {
        //發現player切換至chaseState
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else 
        { 
            currentEnemy.anim.SetBool("walk", true); 
        }
    }

    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit()
    {
        
    }
}
