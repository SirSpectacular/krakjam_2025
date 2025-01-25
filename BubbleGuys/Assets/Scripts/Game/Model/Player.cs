using System;
using UnityEngine;

namespace Game.Model
{
    public class Player
    {
        private float _volume;
        public static  readonly float MinVolume = Mathf.PI / 4;
        public float Diameter => Mathf.Sqrt(Volume / Mathf.PI) * 2;

        public float Volume
        {
            get => _volume;
            set
            {
                if (_volume <= MinVolume)
                {
                    _volume = MinVolume;
                    return;
                }
                _volume = value;
            }
        }
        
        public Player(float initVolume)
        {
            _volume = initVolume;
        }
    }
}