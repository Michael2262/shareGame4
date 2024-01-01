using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自BaseState的抽象方法
public class BoarPartolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        //切換狀態
        if (currentEnemy.FoundPlayer()) 
        {
            currentEnemy.switchState(NPCState.Chase);
        }
        
        
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
