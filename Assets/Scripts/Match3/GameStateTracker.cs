using UnityEngine;
using System.Collections;

namespace Match3
{
    public class GameStateTracker : MonoBehaviour
    {
        [SerializeField] private GameStateVariable gameState;
        [SerializeField] private GameStateUnityEvent onGameStateChanged;


        public void SetGameState(GameState newState)
        {
            if (newState != gameState)
            {
                gameState.Value = newState;
                onGameStateChanged.Invoke(newState);
            }
        }
    }
}