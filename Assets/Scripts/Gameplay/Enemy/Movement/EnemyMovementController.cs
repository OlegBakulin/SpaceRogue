using Abstracts;

namespace Gameplay.Enemy.Movement
{
    public class EnemyMovementController : BaseController
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _model;

        public EnemyMovementController(EnemyView view, EnemyMovementModel model)
        {
            _view = view;
            _model = model;
        }
        
        //TODO add model-view linking methods
    }
}