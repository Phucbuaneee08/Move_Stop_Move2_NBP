using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AttackState : IState
{

    public void OnEnter(Character t)
    {
        t.FocusTarget();
        t.Attack();
    }

    public void OnExecute(Character t)
    {
        t.ResetAttack();
    }

    public void OnExit(Character t)
    {
        
    }
}
