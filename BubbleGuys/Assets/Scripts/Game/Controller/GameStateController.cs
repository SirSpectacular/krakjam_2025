using System;
using Game.Model;
using UnityEngine;

namespace Game.Controller
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private GameObject _finishPanel;
        private void Awake()
        {
            Time.timeScale = 0.0f;
        }

        private void Update()
        {
            if (DataProvider.Instance == null || _finishPanel.activeSelf)
            {
                return;
            }

            GameState gameState = DataProvider.Instance.GameState;
            if (gameState.IsFinished)
            {
                Time.timeScale = 0.0f;
                _finishPanel.SetActive(true);
            }
            else if(gameState.IsStarted)
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}