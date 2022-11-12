using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
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
        public RaycastHit2D _hit;
        CircleCollider2D _collider;
        GameObject _rectTransform;
        
        bool _activeAtack = false;
        int _timeActiveAtack = 0;
        int _intTimeActiveAtack = 0;

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
            _rectTransform = _weaponConfig.Projectile.Prefab.gameObject;
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
            if (0 == _timeActiveAtack)
            {
                ++_timeActiveAtack;
            }
            else
            {
                _activeAtack = false;
                CooldownTimer.SetMaxValue(3);
                CooldownTimer.Start();
                EntryPoint.UnsubscribeFromUpdate(ActiveAtackTrueOrFalse);
            }
        }

        private void FireLaser()
        {
            _startLaserPosition = _gun.gameObject.transform.position;
            _endLaserPosition = _gun.gameObject.transform.TransformPoint(Vector3.up * _weaponConfig.Long_Dlina);

            _rectTransform.transform.rotation = new Quaternion(0, 0, _playerTransform.rotation.z, _playerTransform.rotation.w);

            _laser.SetPosition(0, _startLaserPosition);
            if ((_hit = Physics2D.Linecast(_startLaserPosition, _endLaserPosition)) && !(_hit.transform.gameObject.GetComponent<ProjectileView>()))
            {
                UnityEngine.Debug.Log(_hit.collider.gameObject.name + " " + _hit.transform.position + " / " + _endLaserPosition.ToString() + " / " + _gun.transform.position.ToString() + " / " + _weaponConfig.Long_Dlina);

                _laser.SetPosition(1, _hit.point);
                _rectTransform.transform.localScale.Set(_rectTransform.transform.localScale.x, _hit.distance, 0);
            }
            else
            {
                _laser.SetPosition(1, _endLaserPosition);
                _rectTransform.transform.localScale.Set(_rectTransform.transform.localScale.x, _weaponConfig.Long_Dlina, 0);
            }
            _collider.radius = _laser.widthMultiplier / 2;
            _collider.offset = (Vector2)(_laser.GetPosition(1) - _laser.GetPosition(0));
            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);
            _timeActiveAtack = 0;

            if (!_activeAtack)
            {
                EntryPoint.SubscribeToUpdate(ActiveAtackTrueOrFalse);
                _activeAtack = true;
            }

            

        
            //CooldownTimer.Start();
        }

    }
}
