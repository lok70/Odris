using Assets.Bekoe_sScripts.NewEnemyVariations.StateMachine.ConcreteStates;
using Assets.Scripts.NewEnemyVariations.StateMachine.ConcreteStates;
using UnityEngine;

public class RotatingMagicEnemy : Enemy
{

    private AtackEnemyState _state;
    private void Awake()
    {
        AtackState = _state;
    }
}
