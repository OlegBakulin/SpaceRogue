using System;
using Gameplay.GameState;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform uiPosition;
    
    private const GameState InitialGameState = GameState.Game;

    private MainController _mainController;
    
    private void Awake()
    {
        var gameState = new CurrentState(InitialGameState);
        _mainController = new MainController(gameState, uiPosition);
        
    }
    
    private void OnDestroy()
    {
        _mainController.Dispose();
    }
    
    
    #region UpdateMechanism

    private static event Action OnUpdate = () => { };
    private static event Action OnFixedUpdate = () => { };
    private static event Action OnLateUpdate = () => { };
    
    public static void SubscribeToUpdate(Action callback) => OnUpdate += callback;
    public static void UnsubscribeFromUpdate(Action callback) => OnUpdate -= callback;
    public static void SubscribeToFixedUpdate(Action callback) => OnFixedUpdate += callback;
    public static void UnsubscribeFromFixedUpdate(Action callback) => OnFixedUpdate -= callback;
    public static void SubscribeToLateUpdate(Action callback) => OnLateUpdate += callback;
    public static void UnsubscribeFromLateUpdate(Action callback) => OnLateUpdate -= callback;
    
    private void Update()
    {
        OnUpdate.Invoke();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate.Invoke();
    }
    private void LateUpdate()
    {
        OnLateUpdate.Invoke();
    }

    #endregion
    
    
}
