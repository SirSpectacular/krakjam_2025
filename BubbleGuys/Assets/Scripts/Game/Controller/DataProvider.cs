using System;
using System.Collections.Generic;
using System.Linq;
using Game.Model;
using UnityEngine;

namespace Game.Controller
{
    public class DataProvider : MonoBehaviour
    {
        [SerializeField] public float _playerDensity;
        [SerializeField] public float _playerVolume;
        
        [SerializeField] public float InitialAir = 10;

        private static DataProvider _instance; 
        public static DataProvider Instance => _instance;

        public readonly GameState GameState = new();

        private readonly Dictionary<int, Model.Player> _players = new();

        public Model.Player GetPlayer(int playerId)
        {
            return _players[playerId];
        }
        
        public void KillPlayer(int playerId)
        {
            GetPlayer(playerId).Kill();
            if (_players.Values.All(p => p.IsAlive == false))
            {
                GameState.Finish(null);
            }
        }
        
        private void Awake()
        {
            for (int i = 1; i <= 3; i++)
            {
                Model.Player player = new(_playerVolume, _playerDensity * _playerVolume);
                _players.Add(i, player);
            }
            _instance = this;
        }

        private void Start()
        {
            for (int i = 1; i <= 3; i++)
            {
                PlayerPowerUps.Instance.AddAir(i, InitialAir);
            }  
        }
    }
}