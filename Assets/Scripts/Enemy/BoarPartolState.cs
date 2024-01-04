using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自BaseState的抽象方法
//野豬的巡邏狀態
public class BoarPartolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {

        //currentEnemy是BaseState定義的一個變數，要求要有Enemy物件
        //呼叫此狀態的物件會把自己身上的Enemy傳進來
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        //切換狀態
        //巡邏狀態的野豬，會做甚麼事情，如果觸發甚麼條件會離開此狀態
        if (currentEnemy.FoundPlayer()) 
        {
            currentEnemy.SwitchState(NPCState.Chase);
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
        //一旦離開此狀態時會觸發
        currentEnemy.anim.SetBool("walk", false);
        Debug.Log("Exit");
    }
}
