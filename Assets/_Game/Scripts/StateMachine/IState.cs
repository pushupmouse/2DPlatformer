using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Enemy enemy);
    void onExecute(Enemy enemy);
    void OnExit(Enemy enemy);
}
