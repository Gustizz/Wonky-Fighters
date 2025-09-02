using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : State
{
    public IdleState idleState;

    public float retreatSpeed = 0.5f;

    public enemyBehaviour enemyBehaviour;

    private bool isRetreating = false;
    public override State RunCurrentState()
    {

        enemyBehaviour.AimWeaponAtEnemy(0);

        if (!isRetreating)
        {
            StartCoroutine(enemyBehaviour.RetreatFromPlayer(retreatSpeed));
        }


        if (isRetreating)
        {
            isRetreating = false;
            return idleState;
        }
        else
        {
            isRetreating = false;
            return this;
        }
    }

    public void ReturnState(bool _bool)
    {
        isRetreating = _bool;
    }
}
