using Gameplay.Player;
using Gameplay.Shooting;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private readonly EnemyView _view;
        private readonly FrontalTurretController _frontalTurret;
        private readonly PlayerView _target;
        
        public EnemyCombatBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view, FrontalTurretController frontalTurret) : base(enemyState)
        {
            _view = view;
            _frontalTurret = frontalTurret;
        }

        protected override void OnUpdate()
        {
            //TODO implement hit-and-run tactics
        }
    }
}