using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalNullGunController : FrontalTurretController
    {
        public FrontalNullGunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
        }

        public override void CommenceFiring()
        {
            UnityEngine.Debug.Log("Mull-gun has fired!");
        }

        public override void CoolDown()
        {
            UnityEngine.Debug.Log("Mull-gun is cooling down");
        }
    }
}