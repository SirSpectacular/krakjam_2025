using System;
using Game.Model;
using UnityEngine;

namespace Game.Controller
{
    public class PlayerPowerUps : MonoBehaviour
    {
        [SerializeField] PowerUpConfig _rocksPowerUpConfig;
        [SerializeField] PowerUpConfig _airPowerUpConfig;
        [SerializeField] PowerUpConfig _helliumPowerUpConfig;

        public float AirDensity => _airPowerUpConfig.Density;
        
        private static PlayerPowerUps _instance;
        public static PlayerPowerUps Instance => _instance;

        [Serializable]
        private class PowerUpConfig
        {
            public float Density;
            public Sprite ParticleShape;
        }

        public void AddRocks(int playerId, float volume)
        {
            DataProvider.Instance.GetPlayer(playerId).PowerUps.Add(new Rocks(volume, _rocksPowerUpConfig.Density * volume));
        }
        public void AddAir(int playerId, float volume)
        {
            DataProvider.Instance.GetPlayer(playerId).PowerUps.Add(new Air(volume, _airPowerUpConfig.Density * volume));
        }
        
        public void AddHelium(int playerId, float volume)
        {
            DataProvider.Instance.GetPlayer(playerId).PowerUps.Add(new Hellium(volume, _helliumPowerUpConfig.Density * volume));
        }
        
        private void Awake()
        {
            _instance = this;
        }
    }
}