using Abstracts;
using Gameplay;
using Gameplay.GameState;
using UI.MainMenu;
using UnityEngine;


public class MainController : BaseController
{
    private readonly CurrentState _currentState;
    private readonly Transform _uiPosition;

    private GameController _gameController;
    private MainMenuController _mainMenuController;

    public MainController(CurrentState currentState, Transform uiPosition)
    {
        _currentState = currentState;
        _uiPosition = uiPosition;
        
        _currentState.CurrentGameState.Subscribe(OnGameStateChange);
        OnGameStateChange(_currentState.CurrentGameState.Value);
    }

    protected override void OnDispose()
    {
        DisposeAllControllers();
        
        _currentState.CurrentGameState.Unsubscribe(OnGameStateChange);
    }

    private void OnGameStateChange(GameState newState)
    {
        DisposeAllControllers();
        
        switch (newState)
        {
            case GameState.Menu:
                _mainMenuController = new MainMenuController(_currentState, _uiPosition);
                break;
            case GameState.Game:
                _gameController = new GameController(_currentState);
                break;
            case GameState.None:
            default: break;
        }
    }

    private void DisposeAllControllers()
    {
        _gameController?.Dispose();
        _mainMenuController?.Dispose();
    }
    
}