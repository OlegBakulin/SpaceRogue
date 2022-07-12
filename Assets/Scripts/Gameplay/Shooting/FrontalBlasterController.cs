using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalBlasterController : FrontalTurretController
    {
        public FrontalBlasterController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            EntryPoint.SubscribeToUpdate(CoolDown);
        }
        
        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(CoolDown);
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);

            CooldownTimer = Config.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }
    }
}