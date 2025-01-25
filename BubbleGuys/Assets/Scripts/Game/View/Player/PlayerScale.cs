using System;
using Game.Controller;
using UnityEngine;

namespace Game.Player
{
    public class PlayerScale : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        
        private float? _currentDiameter;
        
        public void Update()
        {   
            if (_currentDiameter != null
                && Mathf.Abs(_currentDiameter.Value - DataProvider.Instance.Player.Diameter) <= Mathf.Epsilon)
            {
                return;
            }
            
            _currentDiameter = DataProvider.Instance.Player.Diameter;
            _player.localScale = DataProvider.Instance.Player.Diameter * Vector3.one;
        }
    }
}
