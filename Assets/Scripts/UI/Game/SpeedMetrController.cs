using Abstracts;
using Gameplay.Player.Movement;
using UnityEngine;
using Utilities.ResourceManagement;
using UnityEngine.UI;

public class SpeedMetrController : BaseController
{
    private readonly ResourcePath _speedMetrPath = new ResourcePath("Prefabs/Canvas/Game/SpeedMetr");
    private readonly PlayerMovementModel _movementController;
    private Text textSpeedMetr;

    public SpeedMetrController(Canvas canvas, PlayerMovementModel movementController)
    {
        _movementController = movementController;
        
        textSpeedMetr = ResourceLoader.LoadObject<Text>(_speedMetrPath);
        textSpeedMetr = GameObject.Instantiate<Text>(textSpeedMetr);
        textSpeedMetr.gameObject.transform.parent = canvas.transform;
        RectTransform rectTransformTextSpeedMetr = textSpeedMetr.gameObject.GetComponent<RectTransform>();
        rectTransformTextSpeedMetr.anchoredPosition = Vector3.zero;
        rectTransformTextSpeedMetr.sizeDelta = Vector3.zero;
        textSpeedMetr.gameObject.transform.localScale = Vector3.one;

        EntryPoint.SubscribeToLateUpdate(UpdatedSpeedMet);
    }

    private void UpdatedSpeedMet()
    {
        string speed = "STOP";
        if (_movementController.CurrentSpeed > 0)
        {
            int nowSpeed = (int)(_movementController.CurrentSpeed / _movementController.MaxSpeed * 100);
            speed = nowSpeed.ToString();
        }
        else
        {
            if (_movementController.CurrentSpeed == 0)
            {
                speed = "STOP";
            }
            else 
            {
                if (_movementController.CurrentSpeed < 0)
                {
                    speed = "R";
                }
            }
        }
        textSpeedMetr.text = "SPD: " + speed;
    }

    protected override void OnDispose()
    {
        EntryPoint.UnsubscribeFromLateUpdate(UpdatedSpeedMet);
    }
}
