using StatePattern.Main;
using StatePattern.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace StatePattern.Enemy
{
    public class ShootingState : IState
    {
        public OnePunchManController Owner {get; set; }
        private OnePunchManStateMachine stateMachine;
        private PlayerController target;
        private float shootTimer;

        public ShootingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;
        public void OnStateEnter()
        {
            SetTarget();
            shootTimer = 0;
        }
        public void Update()
        {
            Quaternion desiredQuaternion = CalculateRotationTowardsPlayer();
            Owner.SetRotation(RotateTowards(desiredQuaternion));

            if (IsFacingPlayer(desiredQuaternion))
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    shootTimer = Owner.Data.RateOfFire;
                    Owner.Shoot();
                }
            }
        }
        public void OnStateExit() => target = null;

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();
        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        private Quaternion RotateTowards(Quaternion desiredRotation) => 
            Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, 
                Owner.Data.RotationSpeed / 30 * Time.deltaTime);
        private bool IsFacingPlayer(Quaternion desiredRotation) => 
            Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;
    }
}