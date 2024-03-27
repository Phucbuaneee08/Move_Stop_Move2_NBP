using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class IdleState : IState
{
    private float timeInterval = 3f;
    private float countTime = 0f;
    public void OnEnter(Bot t)
    {
      

    }

    public void OnExecute(Bot t)
    {
        countTime += Time.deltaTime;
        if (countTime > timeInterval)
            t.ChangeState(new PatrolState());

    }

    public void OnExit(Bot t)
    {
        
    }
}
