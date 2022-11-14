using Gameplay.Player;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalLaserController : FrontalTurretController
    {
        private readonly LaserWeaponConfig _weaponConfig;
        private LineRenderer _laser;
        private Transform _playerTransform;
        private GameObject _gun;
        private Vector3 _endLaserPosition;
        private Vector3 _startLaserPosition;
        private Vector3 _laserLocalScale;
        public RaycastHit2D _hit;
        CircleCollider2D _collider;
        Transform _rectTransform;
        
        bool _activeAtack = true;
        bool _firstAttack = true;
        int _timeActiveAtack;

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var _attackActive = gunPointParentTransform.gameObject.GetComponent<PlayerView>();
            
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("Wrong config type was provided");
            _playerTransform = gunPointParentTransform;
            _gun = _playerTransform.GetChild(0).gameObject;
            _laser = _weaponConfig.Projectile.Prefab.gameObject.GetComponent<LineRenderer>();
            _collider = _weaponConfig.Projectile.Prefab.gameObject.GetComponent<CircleCollider2D>();
            _rectTransform = _weaponConfig.Projectile.Prefab.gameObject.transform;
            _laserLocalScale = _rectTransform.localScale;
        }
         
        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            FireLaser();
        }

        private void ActiveAtackTrueOrFalse()
        {
            if (_activeAtack && _timeActiveAtack <= _weaponConfig.MaxActiveTime_M * 100)
            {
                ++_timeActiveAtack;
                _activeAtack = false;
            }
            else
            {
                CooldownTimer.SetMaxValue(_timeActiveAtack * _weaponConfig.Koefficient_X * 0.01f);
                _firstAttack = true;
                _timeActiveAtack = 0;
                EntryPoint.UnsubscribeFromUpdate(ActiveAtackTrueOrFalse);
                CooldownTimer.Start();
            }
        }

        private void FireLaser()
        {
            _activeAtack = true;
            _startLaserPosition = _gun.gameObject.transform.position;
            _endLaserPosition = _gun.gameObject.transform.TransformPoint(Vector3.up * _weaponConfig.Long_Dlina);

            _rectTransform.rotation = new (0, 0, _playerTransform.rotation.z, _playerTransform.rotation.w);

            _laser.SetPosition(0, _startLaserPosition);
            if ((_hit = Physics2D.Linecast(_startLaserPosition, _endLaserPosition)) && !(_hit.transform.gameObject.GetComponent<ProjectileView>()))
            {
                _laser.SetPosition(1, _hit.point);
                _laserLocalScale.Set(_laserLocalScale.x, _hit.distance, 0);
            }
            else
            {
                _laser.SetPosition(1, _endLaserPosition);
                _laserLocalScale.Set(_laserLocalScale.x, _weaponConfig.Long_Dlina, 0);
            }

            _collider.radius = _laser.widthMultiplier / 2;
            _collider.offset = (Vector2)(_laser.GetPosition(1) - _laser.GetPosition(0));
            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);

            if (_firstAttack)
            {
                EntryPoint.SubscribeToUpdate(ActiveAtackTrueOrFalse);
                _firstAttack = false;
            }
        }

    }
}
