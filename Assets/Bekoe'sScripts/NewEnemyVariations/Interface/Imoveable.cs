using UnityEngine;
using UnityEngine.AI;

public interface Imoveable
{
    NavMeshAgent agent { get; set; }
    void moveEnemy(Vector2 velocity);
}
