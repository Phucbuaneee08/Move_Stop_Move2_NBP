using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Character t)
    {
        t.RandomMove();
    }

    public void OnExecute(Character t)
    {
        t.Stop();
    }

    public void OnExit(Character t)
    {
       
    }
}
