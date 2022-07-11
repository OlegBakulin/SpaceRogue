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

    private readonly float _CurrentSpeed;
    private readonly float _MaxSpeed;
    private readonly HealthController _healthController;
    PlayerStatusBarController playerStatusBarController;
    SpeedoMetrController speedMetrController;
        
    public CanvasController(HealthController healthController, float CurrentSpeed, float MaxSpeed)
    {
        _CurrentSpeed = CurrentSpeed;
        _MaxSpeed = MaxSpeed;
        _healthController = healthController;

        Canvas canvase = LoadView<Canvas>(canvasPath);

        playerStatusBarController = new PlayerStatusBarController(_healthController.HealthModel, canvase);
        AddController(playerStatusBarController);

        speedMetrController = new SpeedoMetrController(canvase, _CurrentSpeed, _MaxSpeed);
        AddController(speedMetrController);
    }

}
