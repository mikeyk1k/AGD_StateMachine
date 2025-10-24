using StatePattern.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public class GenericStateMachine<T> where T : EnemyController
    {
        protected T owner;
        protected IState currentState;
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();

        public GenericStateMachine(T owner) => this.owner = owner;

        protected void SetOwner()
        {
            foreach (var state in States.Values)
            {
                state.Owner = owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
        public void ChangeState(States newState) => ChangeState(States[newState]);
    }
    public enum States
    {
        IDLE,
        ROTATING,
        SHOOTING,
        PATROLLING,
        CHASING
    }
}