using System;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public abstract class EnemyBehaviour : IDisposable
    {
        private readonly SubscribedProperty<EnemyState> _enemyState;
        private readonly PlayerView _playerView;
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            OnDispose();
            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
        }

        protected EnemyBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView)
        {
            _enemyState = enemyState;
            _playerView = playerView;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void ChangeState(EnemyState newState)
        {
            if (newState != _enemyState.Value) _enemyState.Value = newState;
        }
        
        protected abstract void OnUpdate();

        protected virtual void OnDispose() { }
    }
}