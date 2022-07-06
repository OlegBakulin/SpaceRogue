using Abstracts;
using Gameplay.GameState;
using Gameplay.Health;
using Gameplay.Player;
using Gameplay.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ResourceManagement;

public class CanvasController : BaseController
{


    private readonly ResourcePath canvasPath = new ResourcePath("Prefabs/Canvas/Canvas");

    private readonly PlayerMovementModel _movementModel;
    private readonly HealthController _healthController;
    PlayerStatusBarController playerStatusBarController;
    SpeedMetrController speedMetrController;
        
    public CanvasController(PlayerMovementModel movementModel, HealthController healthController)
    {
        _movementModel = movementModel;
        _healthController = healthController;

        Canvas canvase = LoadView<Canvas>(canvasPath);

        playerStatusBarController = new PlayerStatusBarController(_healthController.HealthModel, canvase);
        AddController(playerStatusBarController);

        speedMetrController = new SpeedMetrController(canvase, _movementModel);
        AddController(speedMetrController);
    }

}
