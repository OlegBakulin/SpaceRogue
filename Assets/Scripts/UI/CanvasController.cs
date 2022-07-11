using Abstracts;
using Gameplay.Health;
using Gameplay.Player.Movement;
using UnityEngine;
using Utilities.ResourceManagement;

public class CanvasController : BaseController
{


    private readonly ResourcePath canvasPath = new ResourcePath("Prefabs/Canvas/Canvas");

    private readonly HealthController _healthController;
    private readonly PlayerStatusBarController playerStatusBarController;
    private readonly SpeedoMetrController speedMetrController;
        
    public CanvasController(HealthController healthController, PlayerMovementController playerMovementController)
    {
        _healthController = healthController;

        Canvas canvase = LoadView<Canvas>(canvasPath);

        playerStatusBarController = new PlayerStatusBarController(_healthController.HealthModel, canvase);
        AddController(playerStatusBarController);

        speedMetrController = new SpeedoMetrController(canvase, playerMovementController);
        AddController(speedMetrController);
    }

}
