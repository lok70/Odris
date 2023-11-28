
using UnityEngine;

namespace Assets.Bekoe_sScripts.NewEnemyVariations.StateMachine.ConcreteStates
{

    public class PatrolEnemyState : EnemyState
    {
        private float timer;
        private Vector2 startPosition;
        private float patrolRadius = 3.5f;
        private Vector2 currentPoint;
     
        public PatrolEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            startPosition = enemy.transform.position;
            currentPoint = (Vector2)enemy.transform.position +  Random.insideUnitCircle * patrolRadius;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            enemy.agent.SetDestination(currentPoint);
            if (!enemy.obctacklesChecker(currentPoint))
            {
                if ((Vector2)enemy.transform.position == currentPoint)
                {
                    timer += Time.deltaTime;
                    if (timer > Random.Range(2, 7))
                    {
                        
                        currentPoint = startPosition + Random.insideUnitCircle * patrolRadius;
                        Debug.Log("Таймер закончился, выполняется смена точки");
                        timer = 0;
                    }

                }
            }
            else
            {
                currentPoint = startPosition + Random.insideUnitCircle * patrolRadius;
            }


            if (enemy.distanceFromPlayer <= enemy.shootingDistance & !enemy.obstackleFlag)
            {
                enemyStateMachine.ChangeState(enemy.AtackState);
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        
        private bool CheckSpeed()
        {
            return enemy.agent.speed > 0.1;
        }

       
    }
}
