using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        //½á­È
        patrolState = new SnailPartolState();
        //chaseState = new SnailChaseState();
    }
}
