using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�~�Ӧ�BaseState����H��k
//���ު����ު��A
public class BoarPartolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {

        //currentEnemy�OBaseState�w�q���@���ܼơA�n�D�n��Enemy����
        //�I�s�����A������|��ۤv���W��Enemy�Ƕi��
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        //�������A
        //���ު��A�����ޡA�|���ƻ�Ʊ��A�p�GĲ�o�ƻ����|���}�����A
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
        //�@�����}�����A�ɷ|Ĳ�o
        currentEnemy.anim.SetBool("walk", false);
        Debug.Log("Exit");
    }
}
