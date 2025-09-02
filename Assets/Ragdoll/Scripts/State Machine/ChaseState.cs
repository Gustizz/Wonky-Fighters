using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    public AttackState attackState;
    public IdleState idlesState;
    public bool isInAttackRange;

    public int attackType;
    public float chaseSpeed;
    
    public enemyBehaviour enemyBehaviour;

    
    public override State RunCurrentState()
    {
        enemyBehaviour.MoveTowardsPlayer(chaseSpeed);

        switch (attackType)
        {
            //Overhand
            case 0:
                enemyBehaviour.AimWeaponAtEnemy(0);
                break;
            //Underarm
            case 1:
                enemyBehaviour.AimWeaponAtEnemy(-4);
                break;
            //Stab
            case 2:
                enemyBehaviour.StabChase(-2);
                break;
        }
        

        float dist = enemyBehaviour.DistanceFromPlayer();

        
        if (dist < 2.5f)
        {
            attackState.attackType = attackType;
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
