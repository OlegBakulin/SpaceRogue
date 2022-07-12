using System;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public abstract class EnemyBehaviour : IDisposable
    {
        private readonly SubscribedProperty<EnemyState> _enemyState;
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
            OnDispose();
        }

        protected EnemyBehaviour(SubscribedProperty<EnemyState> enemyState)
        {
            _enemyState = enemyState;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void ChangeState(EnemyState newState)
        {
            _enemyState.Value = newState;
        }
        
        protected abstract void OnUpdate();

        protected virtual void OnDispose() { }
    }
}