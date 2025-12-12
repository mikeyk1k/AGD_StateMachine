using StatePattern.StateMachine;
using System.Collections;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateClone();
            CreateClone();
        }
        public void Update()
        {
            throw new System.NotImplementedException();
        }
        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        private void CreateClone()
        {
            throw new System.NotImplementedException();
        }
    }
}