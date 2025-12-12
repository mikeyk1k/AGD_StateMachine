using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public TeleportingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(States.CHASING);
        }
        public void Update() { }
        public void OnStateExit() { }

        private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPosition(5f));
        private Vector3 GetRandomNavMeshPosition(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += Owner.Position;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
                return hit.position;
            return Owner.Data.SpawnPosition;
        }
    }
}