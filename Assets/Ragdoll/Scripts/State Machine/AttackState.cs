using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public RetreatState retreatState;
    public enemyBehaviour enemyBehaviour;

    private bool isAttackFinsihed = false;

    public float attackPower = 0.2f;
    
    public int attackType;
    
    public override State RunCurrentState()
    {
        enemyBehaviour.MoveTowardsPlayer(0);

        if (!isAttackFinsihed)
        {
            
            switch (attackType)
            {
                //Overhand
                case 0:
                    StartCoroutine(enemyBehaviour.AttackOverHand(attackPower));
                    break;
                //Underarm
                case 1:
                    StartCoroutine(enemyBehaviour.AttackUnderArm(attackPower));
                    break;
                //Stab
                case 2:
                    StartCoroutine(enemyBehaviour.AttackStab(attackPower));
                    break;
            }
            
        }
        
        

        if (isAttackFinsihed)
        {
            isAttackFinsihed = false;
            return retreatState;
        }
        else
        {
            isAttackFinsihed = false;
            return this;
        }


    }

    public void ReturnState(bool _bool)
    {
        isAttackFinsihed = _bool;
    }
}
