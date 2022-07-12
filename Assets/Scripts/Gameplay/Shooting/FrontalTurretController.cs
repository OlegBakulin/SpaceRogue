using Abstracts;
using Scriptables.Modules;
using UnityEngine;
using Utilities.ResourceManagement;

namespace Gameplay.Shooting
{
    public abstract class FrontalTurretController : BaseController
    {
        public bool IsOnCooldown => CooldownTimer > 0;
        
        protected float CooldownTimer;

        protected readonly TurretModuleConfig Config;
        protected readonly ProjectileFactory ProjectileFactory;

        private readonly ResourcePath _gunPointPrefab = new("Prefabs/Stuff/GunPoint");
        

        public FrontalTurretController(TurretModuleConfig config, Transform gunPointParentTransform)
        {
            Config = config;
            var gunPointView = ResourceLoader.LoadPrefab(_gunPointPrefab);
            
            var turretPoint = Object.Instantiate(
                gunPointView, 
                gunPointParentTransform.TransformDirection(Vector3.up * gunPointParentTransform.localScale.y), 
                gunPointParentTransform.rotation
            );
            turretPoint.transform.parent = gunPointParentTransform;
            
            ProjectileFactory = new ProjectileFactory(Config.ProjectileConfig, Config.ProjectileConfig.Prefab, turretPoint.transform);
            
            CooldownTimer = 0.0f;
            
            AddGameObject(turretPoint);
        }

        public abstract void CommenceFiring();

        public abstract void CoolDown();

        protected void BasicCoolDown()
        {
            switch (CooldownTimer)
            {
                case 0:
                    return;
                case < 0:
                    CooldownTimer = 0;
                    return;
                case > 0:
                    CooldownTimer -= Time.deltaTime;
                    return;
            }
        }
    }
}