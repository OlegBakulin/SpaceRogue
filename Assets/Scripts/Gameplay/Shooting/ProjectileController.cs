using Abstracts;
using Gameplay.Damage;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class ProjectileController : BaseController
    {
        private readonly ProjectileConfig _config;
        private readonly DamageModel _damageModel;
        private readonly ProjectileView _view;
        private readonly Vector3 _movementDirection;
        private float _remainingLifeTime;
        
        public ProjectileController(ProjectileConfig config, ProjectileView view, Vector3 movementDirection)
        {
            _config = config;
            _movementDirection = movementDirection;
            _view = view;
            AddGameObject(_view.gameObject);
            _damageModel = new DamageModel(config.DamageAmount);
            _remainingLifeTime = config.LifeTime;
            
            //TODO init view and subscribe to onCollisionEnter when damage model is developed
            _view.Init();

            EntryPoint.SubscribeToUpdate(TickDown);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(TickDown);
        }

        private void TickDown()
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }

            var transform = _view.transform;
            transform.position += _movementDirection * (_config.Speed * 2 * Time.deltaTime);
            
            _remainingLifeTime -= Time.deltaTime;
        }
    }
}