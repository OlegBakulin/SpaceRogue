using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemySeekingPlayerBehaviour : EnemyBehaviour
    {
        private readonly EnemyView _view;
        
        public EnemySeekingPlayerBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view) : base(enemyState)
        {
            _view = view;
        }

        protected override void OnUpdate()
        {
            //TODO implement seeking or chasing behaviour
        }
    }
}