using Unity.VisualScripting;
using UnityEngine;

public class GuideStateMachine : MonoBehaviour
{
    public GuideState currentState { get; set; }

    public void Initialize(GuideState startState)
    {
        currentState = startState;
        //вход в состояние методом из состояния
    }

    public void ChangeState(GuideState nextState)
    {
        //выход из состояние методом из состояния
        currentState = nextState;
        //вход в состояние методом из состояния
    }
}
