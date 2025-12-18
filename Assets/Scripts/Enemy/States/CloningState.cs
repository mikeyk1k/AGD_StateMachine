using StatePattern.Main;
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
        public void Update() { }
        public void OnStateExit() { }

        private void CreateClone()
        {
            RobotController robotOwner = Owner as RobotController;
            if (robotOwner.CloneCountLeft <= 0)
                return;
            RobotController clone = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            clone.SetCloneCount(robotOwner.CloneCountLeft - 1);
            clone.Teleport();
            clone.SetDefaultColor(EnemyColorType.Clone);
            clone.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clone);
        }
    }
}