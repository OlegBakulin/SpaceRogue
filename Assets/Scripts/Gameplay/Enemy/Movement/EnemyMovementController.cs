namespace Gameplay.Enemy.Movement
{
    public class EnemyMovementController
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _model;

        public EnemyMovementController(EnemyView view, EnemyMovementConfig config)
        {
            _view = view;
            _model = new EnemyMovementModel(config);
        }
        
        //TODO add model-view linking methods
    }
}