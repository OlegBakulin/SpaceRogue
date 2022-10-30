using Gameplay.Mechanics.Timer;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{
    [RequireComponent(typeof(LineRenderer))]
    public class FrontalLaserController : FrontalTurretController
    {
        private readonly LaserWeaponConfig _weaponConfig;
        private LineRenderer _line;
        private LineRenderer _laser;
        private Transform _playerTransform;
        private GameObject _gun;
        private Vector3 _endLaserPosition;

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            _gun = gunPointParentTransform.GetChild(0).gameObject;
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("Wrong config type was provided");
            _playerTransform = gunPointParentTransform;
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            } 

            FireLaserProjectiles(_weaponConfig.MaxActiveTime_M, _weaponConfig.Long_Dlina);
            //CooldownTimer.Start();
        }

        private void FireLaserProjectiles(float activeTime, float liserLong)
        {
            if (_laser) GameObject.Destroy(_laser.gameObject);
            _endLaserPosition = (_gun.transform.position + _gun.gameObject.transform.TransformDirection(_gun.transform.localPosition + new Vector3(0, liserLong, 0))); //new Vector3 (_gun.transform.localPosition.x * _playerTransform.rotation.z, (_gun.transform.localPosition.y + liserLong) * _playerTransform.rotation.z, 0));
            _line = _weaponConfig.LaserProjectile.Prefab.GetComponent<LineRenderer>();
            _line.SetPosition(0, _gun.transform.position);
            //ProjectileController projectile = ProjectileFactory.CreateProjectile();
            _laser = GameObject.Instantiate(_line, _gun.transform.position, _gun.transform.rotation);
            RaycastHit hit;
            if (Physics.Raycast(_endLaserPosition, _gun.transform.position, out hit, liserLong))
            {
                _line.SetPosition(1, hit.point);
            }
            else
            {
                _line.SetPosition(1, _endLaserPosition);
            }
            /*
            
            
            
            _line.SetPosition(1, _gun.transform.parent.localPosition);
            _line.SetPosition(0, _line.GetPosition(0) + new Vector3(0, 10,0));//_gun.transform.localPosition);
            
            
                       
                        */

            //AddController(projectile);
        }
    }
}
