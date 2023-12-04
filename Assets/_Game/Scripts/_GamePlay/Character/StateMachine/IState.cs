using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Character t);
    void OnExecute(Character t);
    void OnExit(Character t);
}