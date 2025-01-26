using System;
using Game.Controller;
using UnityEngine;

namespace Game.View.Player
{
    public class PlayerAliveStatus : MonoBehaviour
    {
        [SerializeField] private PlayerIdProvider _playerId;
        [SerializeField] private GameObject _playerObject;

        private bool? _currentAliveStatus;
        private Model.Player _player;
        private void Start()
        {
            _player = DataProvider.Instance.GetPlayer(_playerId);
        }

        private void Update()
        {
            bool isAlive = _player.IsAlive;
            if (_currentAliveStatus == isAlive)
            {
                return;
            }
            _currentAliveStatus = isAlive;
            if (_playerObject != null)
            {
                _playerObject.SetActive(isAlive);
            }
        }
    }
}