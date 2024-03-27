using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AttackState : IState
{
    private float countTime = 0f;
    private float timeInterval = 2f;
    public void OnEnter(Bot t)
    {

        t.OnMoveStop();
        if (Utilities.Chance(60, 100))
        {
            t.OnAttack();
        }
        else
        {
            t.ChangeState(new PatrolState());
        }
        //t.OnAttack();
    }

    public void OnExecute(Bot t)
    {
        //countTime += Time.deltaTime;
        //if (countTime > timeInterval)
        //    t.ResetAttack();
    }

    public void OnExit(Bot t)
    {
        
    }
}
