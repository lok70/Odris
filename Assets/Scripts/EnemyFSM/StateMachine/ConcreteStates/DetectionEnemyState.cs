using System.Collections;
using UnityEngine;

namespace Assets.Scripts.NewEnemyVariations.StateMachine.ConcreteStates
{
    public class DetectionEnemyState : EnemyState
    {
        
        public DetectionEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.agent.speed = 3.5f;
            Debug.Log("Противник заметил игрока");
        }

        public override void ExitState()
        {
            base.ExitState();

        }
        
        public override void FrameUpdate()
        {
            base.FrameUpdate();
            enemy.moveEnemy(enemy.target.transform.position);

            if (enemy.distanceFromPlayer <= enemy.chasingDistance && !enemy.obstackleFlag)
            {
                enemyStateMachine.ChangeState(enemy.ChaseState);
            }
            if ((enemy.distanceFromPlayer > enemy.aggroDistanse && enemy.obstackleFlag) || (enemy.distanceFromPlayer > enemy.chasingDistance && !enemy.obstackleFlag))
            {
                Debug.Log("Противник покинул поле зрения");
                enemy.lastTargetPoint = enemy.target.transform.position;
                enemyStateMachine.ChangeState(enemy.LastPointCheckState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            enemy.moveEnemy(enemy.target.transform.position);
        }

        
    }
}
