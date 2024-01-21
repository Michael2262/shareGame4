using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BeeChaseState : BaseState
{
    private Attack attack;
    private Vector3 target;
    private Vector3 moveDir;
    private bool isAttack;
    private float attackRateCounter =0;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        attack = enemy.GetComponent<Attack>();
        
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);


        //§PÂ_§ðÀ»¶ZÂ÷
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.attackRange)
        {
            {
                //§ðÀ»
                isAttack = true;
                if(!currentEnemy.isHurt)
                    currentEnemy.rb.velocity = Vector2.zero;

                //­p®É¾¹
                attackRateCounter -= Time.deltaTime;
                if (attackRateCounter <= 0)
                {
                    currentEnemy.anim.SetTrigger("attack");
                    attackRateCounter = attack.attackRate;
                }
            }
        }
        else { isAttack = false; }

        moveDir = (target - currentEnemy.transform.position).normalized;


        if (moveDir.x > 0)
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if (moveDir.x < 0)
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);

    }
    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isHurt && !currentEnemy.wait && !isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
    }


    public override void OnExit()
    {
        //Debug.Log("EXIT");
    }

}
