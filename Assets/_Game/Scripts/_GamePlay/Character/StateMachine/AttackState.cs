using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AttackState : IState
{
    private float countTime = 0f;
    private float timeInterval = 2f;
    public void OnEnter(Character t)
    {
        t.ChangeAnim(Const.ANIM_ATTACK);
        t.FocusTarget();
        t.Attack();
    }

    public void OnExecute(Character t)
    {
        countTime += Time.deltaTime;
        if (countTime > timeInterval)
            t.ResetAttack();
    }

    public void OnExit(Character t)
    {
        
    }
}
