using System;
using Game.Controller;
using UnityEngine;

namespace Game.Player
{
    public class PlayerScale : MonoBehaviour
    {
        [SerializeField] private int _playerId;
        [SerializeField] private Transform _player;
        [SerializeField] private float _factor = 0.5f;
        
        private float? _currentDiameter;
        
        public void Update()
        {
            float newDiameter = DataProvider.Instance.GetPlayer(_playerId).Diameter;
            if (_currentDiameter != null
                && Mathf.Abs(_currentDiameter.Value - newDiameter) <= Mathf.Epsilon)
            {
                return;
            }

            _currentDiameter = newDiameter;
            _player.localScale = newDiameter * Vector3.one * _factor;
        }
    }
}
