using Abstracts;
using Gameplay.Health;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ResourceManagement;

public class PlayerStatusBarController : BaseController
{
    public PlayerStatusBarView PlayerStatusBarView => healthShieldToolBarView;
    private readonly ResourcePath healtShieldToolBarPath = new("Prefabs/Canvas/Game/PlayerStatusBar");

    private readonly PlayerStatusBarView healthShieldToolBarView;
    private readonly HealthModel playerHealthShieldModel;
    private readonly Transform barTransform;

    private Slider healthToolBarSlider;
    private Slider shieldToolBarSlider;

    public PlayerStatusBarController(HealthModel healthModel, Canvas canvas)
    {
        healthShieldToolBarView = LoadView<PlayerStatusBarView>(healtShieldToolBarPath);
        playerHealthShieldModel = healthModel;

        healthShieldToolBarView.transform.SetParent(canvas.transform);

        barTransform = healthShieldToolBarView.gameObject.transform;
        healthToolBarSlider = Object.Instantiate(healthShieldToolBarView.HealthSlider, barTransform.transform, true);
        shieldToolBarSlider = Object.Instantiate(healthShieldToolBarView.ShieldSlider, barTransform.transform, true);

        barTransform.localScale = Vector3.one;
        RectTransform rectTransformToolBar = healthShieldToolBarView.gameObject.GetComponent<RectTransform>();
        rectTransformToolBar.anchoredPosition = Vector3.zero;
        rectTransformToolBar.sizeDelta = Vector3.zero;

        EntryPoint.SubscribeToLateUpdate(UpdateHealtShieldToolBar);
    }

    protected override void OnDispose()
    {
        EntryPoint.SubscribeToLateUpdate(UpdateHealtShieldToolBar);
    }

    public void UpdateHealtShieldToolBar()
    {
        if (healthToolBarSlider.maxValue != playerHealthShieldModel.MaximumHealth)
        {
            healthToolBarSlider.maxValue = playerHealthShieldModel.MaximumHealth;
        }

        healthToolBarSlider.value = playerHealthShieldModel.CurrentHealth;


        if (shieldToolBarSlider.maxValue != playerHealthShieldModel.MaximumShield)
        {
            shieldToolBarSlider.maxValue = playerHealthShieldModel.MaximumShield;
        }

        shieldToolBarSlider.value = playerHealthShieldModel.CurrentShield;
    }
}
