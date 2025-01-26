using System;
using Game.Model;
using UnityEngine;

namespace Game.Controller
{
    public class GameStateController : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 0.0f;
        }

        private void Update()
        {
            if (DataProvider.Instance == null)
            {
                return;
            }

            GameState gameState = DataProvider.Instance.GameState;
            if (gameState.IsFinished)
            {
                Time.timeScale = 0.0f;
            }
            else if(gameState.IsStarted)
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}