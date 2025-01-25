using System;
using System.Collections.Generic;
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

        private readonly Dictionary<int, Model.Player> _players = new();

        public Model.Player GetPlayer(int playerId)
        {
            return _players[playerId];
        }
        
        private void Awake()
        {
            for (int i = 1; i <= 4; i++)
            {
                Model.Player player = new(_playerVolume, _playerDensity * _playerVolume);
                _players.Add(i, player);
            }
            _instance = this;
        }

        private void Start()
        {
            for (int i = 1; i <= 4; i++)
            {
                PlayerPowerUps.Instance.AddAir(i, InitialAir);
            }  
        }
    }
}