using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Bot t);
    void OnExecute(Bot t);
    void OnExit(Bot t);
}