using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState currentState { get; set; }

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(EnemyState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
}
