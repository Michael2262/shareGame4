
using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        currentEnemy.GetComponent<Character>().currentDefence = currentEnemy.GetComponent<Character>().specialDefense;

        currentEnemy.anim.SetBool("hide", true);
        currentEnemy.anim.SetTrigger("skill");

        //currentEnemy.GetComponent<Character>().invulnerable = true;


    }
    public override void LogicUpdate()
    {
        //currentEnemy.GetComponent<Character>().invulnerable = true;

        if (currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);
    }


    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("hide", false);
        currentEnemy.GetComponent<Character>().currentDefence = currentEnemy.GetComponent<Character>().defence;
    }
}
