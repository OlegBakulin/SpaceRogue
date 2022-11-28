using Gameplay.Player;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalLaserController : FrontalTurretController
    {
        private readonly LaserWeaponConfig _weaponConfig;
        private LineRenderer _laser;
        private Vector2 _endLaserPosition;
        private Transform _startLaserTransform;
        private Vector3 _startLaserPosirion;
        private RaycastHit2D _hit;
        private Transform _gunPosition;
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

            _startLaserTransform = gunPointParentTransform;
            _gunPosition = base.ProjectileFactory._projectileSpawnTransform.transform;
            _laser = GameObject.Instantiate<LineRenderer>(_weaponConfig.LaserLineRender);
            _laser.enabled = false;

            _collider = _weaponConfig.ProjectileCollider;
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

        private void FireLaser()
        {
            if (_firstAttack)
            {
                EntryPoint.SubscribeToUpdate(ActiveAtackTrueOrFalse);
                _firstAttack = false;
                _laser.enabled = true;
            }

            UpdatedLaserPrivateAttributes();

            FindedHit();
        }

        private void FindedHit()
        {
            if ((_hit = Physics2D.Linecast(_startLaserPosirion, _endLaserPosition)) 
                && !_hit.transform.gameObject.GetComponent<ProjectileView>())
            {
                _laser.SetPosition(1, _hit.point);
            }
            else
            {
                _laser.SetPosition(1, _endLaserPosition);
            }
        }

        private void ActiveAtackTrueOrFalse()
        {
            if (_activeAtack && _timeActiveAtack <= _weaponConfig.MaxLaserActiveTime * 100)
            {
                ++_timeActiveAtack;
                ActivateAttackProjectle();
            }
            else
            {
                UpdatedCooldownTimerAndStopAttack();
                CooldownTimer.Start();
            }
        }

        private void UpdatedCooldownTimerAndStopAttack()
        {
                CooldownTimer.SetMaxValue(_timeActiveAtack * _weaponConfig.KoefficientTimeColldown * 0.01f);
                _firstAttack = true;
                _timeActiveAtack = 0;
                EntryPoint.UnsubscribeFromUpdate(ActiveAtackTrueOrFalse);
                _laser.enabled = false;
        }

        private void ActivateAttackProjectle()
        {
            _activeAtack = false;
            UpdatedConfigsProjectleCollider();
            ProjectileController projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);
        }

        private void UpdatedLaserPrivateAttributes()
        {
            _startLaserPosirion = _startLaserTransform.TransformPoint(_gunPosition.localPosition);
            _endLaserPosition = _startLaserTransform.TransformPoint(Vector3.up * _weaponConfig.LaserLong);
            _activeAtack = true;
            _collider.enabled = false;
            _rectTransform.rotation = new(0, 0, _startLaserTransform.rotation.z, _startLaserTransform.rotation.w);
            _laser.SetPosition(0, _startLaserPosirion);
        }

        private void UpdatedConfigsProjectleCollider()
        {
            _collider.enabled = true;
            _collider.offset = _laser.GetPosition(1) - _laser.GetPosition(0) - _startLaserTransform.TransformPoint(Vector3.up * 0.05f) + _startLaserTransform.position;
        }
    }
}
