using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

public class DeadState : IState
{
    
    public void OnEnter(Character t)
    {
        t.ChangeAnim(Const.ANIM_DEAD);
    }

    public void OnExecute(Character t)
    {
        
    }

    public void OnExit(Character t)
    {
       
    }
}
