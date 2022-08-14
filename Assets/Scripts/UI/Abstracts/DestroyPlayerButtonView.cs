using Gameplay.GameState;
using UnityEngine;

public class DestroyPlayerButtonView : MonoBehaviour
{
    public void BackToMenu(GameObject entryPoint)
    {
        var gameState = new CurrentState(GameState.Menu);
        MainController mainController = new MainController(gameState, entryPoint.transform);
    }
}