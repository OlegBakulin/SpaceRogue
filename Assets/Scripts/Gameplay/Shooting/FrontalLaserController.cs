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
        private Transform _startLaserTransform;
        private Vector3 _laserLocalScale;
        private RaycastHit2D _hit;
        private CircleCollider2D _collider;
        private Transform _rectTransform;

        private bool _activeAtack = true;
        private bool _firstAttack = true;
        private int _timeActiveAtack;

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
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
            _startLaserTransform = _gun.gameObject.transform;
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
            _endLaserPosition = _startLaserTransform.TransformPoint(Vector3.up * _weaponConfig.Long_Dlina);

            _rectTransform.rotation = new (0, 0, _playerTransform.rotation.z, _playerTransform.rotation.w);

            _laser.SetPosition(0, _startLaserTransform.position);
            if ((_hit = Physics2D.Linecast(_startLaserTransform.position, _endLaserPosition)) && !(_hit.transform.gameObject.GetComponent<ProjectileView>()))
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
