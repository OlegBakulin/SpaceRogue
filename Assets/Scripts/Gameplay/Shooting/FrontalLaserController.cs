using Gameplay.Player;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalLaserController : FrontalTurretController
    {
        private readonly LaserWeaponConfig _weaponConfig;
        private LineRenderer _laser;
        private GameObject _laserObject;
        private Transform _playerTransform;
        private GameObject _gun;
        private Vector3 _endLaserPosition;
        private Transform _startLaserTransform;
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
            _startLaserTransform = _gun.gameObject.transform;

            _laserObject = GameObject.Instantiate<GameObject>(_weaponConfig.LaserLineRender.gameObject);
            _laser = _laserObject.GetComponent<LineRenderer>();
            _laser.enabled = false;

            _collider = _weaponConfig.Projectile.Prefab.gameObject.GetComponent<CircleCollider2D>();
            _rectTransform = _weaponConfig.Projectile.Prefab.gameObject.transform;
            _collider.radius = _laser.widthMultiplier / 2;
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
            if (_activeAtack && _timeActiveAtack <= _weaponConfig.MaxLaserActiveTime * 100)
            {
                ++_timeActiveAtack;
                _activeAtack = false;
                _collider.offset = (Vector2)(_laser.GetPosition(1) - _laser.GetPosition(0));
                _collider.enabled = true;
                var projectile = ProjectileFactory.CreateProjectile();
                AddController(projectile);
            }
            else
            {
                CooldownTimer.SetMaxValue(_timeActiveAtack * _weaponConfig.KoefficientTimeColldown * 0.01f);
                _firstAttack = true;
                _timeActiveAtack = 0;
                EntryPoint.UnsubscribeFromUpdate(ActiveAtackTrueOrFalse);
                _laser.enabled = false;
                CooldownTimer.Start();
            }
        }

        private void FireLaser()
        {
            if (_firstAttack)
            {
                EntryPoint.SubscribeToUpdate(ActiveAtackTrueOrFalse);
                _firstAttack = false;
                _laser.enabled = true;
            }

            _endLaserPosition = _startLaserTransform.TransformPoint(Vector3.up * _weaponConfig.LaserLong);
            _activeAtack = true;
            _collider.enabled = false;

            _rectTransform.rotation = new(0, 0, _playerTransform.rotation.z, _playerTransform.rotation.w);

            _laser.SetPosition(0, _startLaserTransform.position);

            if (!NewHit())
            {
                _laser.SetPosition(1, _endLaserPosition);
            }
        }

        private bool NewHit()
        {
            for (int i = 1; i <= 10; i++)
            {
                if ((_hit = Physics2D.Linecast(_startLaserTransform.position, _startLaserTransform.TransformPoint(Vector3.up * _weaponConfig.LaserLong / 10 * i) )) && !(_hit.transform.gameObject.GetComponent<ProjectileView>()))
                {
                    _laser.SetPosition(1, _hit.point);
                    return true;
                }
            }
            return false;
        }


    }
}
