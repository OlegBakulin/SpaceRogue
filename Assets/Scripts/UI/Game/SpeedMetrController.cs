using Abstracts;
using Gameplay.Player.Movement;
using UnityEngine;
using Utilities.ResourceManagement;
using UnityEngine.UI;

public class SpeedoMetrController : BaseController
{
    private readonly ResourcePath _speedMetrPath = new ResourcePath("Prefabs/Canvas/Game/SpeedMetr");
    private readonly float _CurrentSpeed;
    private readonly float _MaxSpeed;
    private Text textSpeedMetr;

    public SpeedoMetrController(Canvas canvas, float CurrentSpeed, float MaxSpeed)
    {
        _CurrentSpeed = CurrentSpeed;
        _MaxSpeed = MaxSpeed;

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
        if (_CurrentSpeed >= 0)
        {
            int nowSpeed = (int)(_CurrentSpeed / _MaxSpeed * 100);
            speed = nowSpeed.ToString();
        }
        else
        {
            speed = "R";      
        }
        textSpeedMetr.text = "SPD: " + speed;
    }

    protected override void OnDispose()
    {
        EntryPoint.UnsubscribeFromLateUpdate(UpdatedSpeedMet);
    }
}
