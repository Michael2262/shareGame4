using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�~�Ӧ�BaseState����H��k
public class BoarPartolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        //�������A
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
