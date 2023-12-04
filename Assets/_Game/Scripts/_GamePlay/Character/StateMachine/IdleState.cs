using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim(Const.ANIM_IDLE);
        t.ChooseTarget();
    }

    public void OnExecute(Character t)
    {
       
    }

    public void OnExit(Character t)
    {
        
    }
}
