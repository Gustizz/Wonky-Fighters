using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool isPlayerInRange;

    public float sprintSpeed = -0.4f;
    public float walkSpeed = -0.4f;
    
    public enemyBehaviour enemyBehaviour;
    public override State RunCurrentState()
    {
        if (enemyBehaviour.canMove)
        {
            float dist = enemyBehaviour.DistanceFromPlayer();

            chaseState.attackType = Random.Range(0, 3);

        
            if (dist > 4)
            {
            
                chaseState.chaseSpeed = sprintSpeed;
                return chaseState;

            }
            else if (dist < 4)
            {
                chaseState.chaseSpeed = walkSpeed;
                return chaseState;
            }
            else
            {
                return this;
            }
        }
        else
        {
            return this;
        }
        
    }
}
