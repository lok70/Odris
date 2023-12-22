using Unity.VisualScripting;
using UnityEngine;

public class GuideStateMachine : MonoBehaviour
{
    public GuideState currentState { get; set; }

    public void Initialize(GuideState startState)
    {
        currentState = startState;
        //���� � ��������� ������� �� ���������
    }

    public void ChangeState(GuideState nextState)
    {
        //����� �� ��������� ������� �� ���������
        currentState = nextState;
        //���� � ��������� ������� �� ���������
    }
}
