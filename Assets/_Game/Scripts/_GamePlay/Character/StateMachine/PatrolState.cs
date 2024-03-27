using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Bot t)
    {
        t.RandomMove();
    }

    public void OnExecute(Bot t)
    {
        t.StopRandomMove();
    }

    public void OnExit(Bot t)
    {
       
    }
}
