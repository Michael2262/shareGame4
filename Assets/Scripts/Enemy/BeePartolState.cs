using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePartolState : BaseState
{
    private Vector3 target;
    private Vector3 moveDir;

    private float changeTargerCounter = 10f;


    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        target = enemy.GetNewPoint();
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            
        }

        changeTargerCounter -= Time.deltaTime;

        if ((Mathf.Abs(target.x - currentEnemy.transform.position.x)<0.1f && Mathf.Abs(target.y - currentEnemy.transform.position.y)<0.1f) || (changeTargerCounter<=0f))
        {
            currentEnemy.wait = true;
            target = currentEnemy.GetNewPoint();
            changeTargerCounter = 10f;
        }

        moveDir = (target - currentEnemy.transform.position).normalized;


        if (moveDir.x > 0)
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if(moveDir.x < 0)
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);


    }   



    public override void PhysicsUpdate()
    {
        if(!currentEnemy.wait && !currentEnemy.isHurt && !currentEnemy.wait) 
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        else
            currentEnemy.rb.velocity = Vector2.zero;

    }
    public override void OnExit()
    {
        
    }
}
