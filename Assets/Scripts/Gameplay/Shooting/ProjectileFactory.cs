using UnityEngine;

namespace Gameplay.Shooting
{
    public class ProjectileFactory
    {
        private readonly ProjectileConfig _config;
        private readonly ProjectileView _view;
        
        private readonly Transform _projectileSpawnTransform;
        
        public ProjectileFactory(ProjectileConfig projectileConfig, ProjectileView view, Transform projectileSpawnTransform)
        {
            _config = projectileConfig;
            _view = view;
            _projectileSpawnTransform = projectileSpawnTransform;
        }

        public ProjectileController CreateProjectile() => CreateProjectile(Vector3.up);
        public ProjectileController CreateProjectile(Vector3 direction) => new(_config, CreateProjectileView(), _projectileSpawnTransform.parent.TransformDirection(direction));

        private ProjectileView CreateProjectileView() => Object.Instantiate(_view, _projectileSpawnTransform.position, Quaternion.identity);
    }
}