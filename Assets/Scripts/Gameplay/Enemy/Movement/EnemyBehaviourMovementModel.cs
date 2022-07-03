using Gameplay.Player;

namespace Gameplay.Enemy.Movement
{
    public class EnemyBehaviourMovementModel
    {
        private readonly EnemyMovementController _movementController;
        private readonly PlayerView _player;
        
        public EnemyBehaviourMovementModel(EnemyMovementController movementController, PlayerView playerView)
        {
            _movementController = movementController;
            _player = playerView;
        }
        
        //TODO implement behaviour in all bottom methods

        public void MoveTowardsPlayer()
        {
            
        }
        
        

        public void RotateTowardsPlayer()
        {
            
        }

        public void StopMoving()
        {
            
        }

        public void StopTurning()
        {
            
        }
    }
}