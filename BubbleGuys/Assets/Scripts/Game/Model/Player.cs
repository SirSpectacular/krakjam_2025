using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    public class Player
    {
        public readonly float BaseVolume;
        private readonly float _baseMass;
        public float Mass => _baseMass + PowerUps.Sum(p => p.Mass);
        public float Diameter => Mathf.Sqrt(Volume / Mathf.PI) * 2;

        public List<PowerUp> PowerUps = new();
        
        public float Volume => BaseVolume + PowerUps.Sum(p => p.Volume);

        public float SubtractVolume(float volume)
        {
            float volumeLeftToSubtract = volume;
            for (int i = 0; i < PowerUps.Count; i++)
            {
                PowerUp powerUp = PowerUps[i];
                float volumeToSubtract = Mathf.Min(volumeLeftToSubtract / (PowerUps.Count - i), powerUp.Volume);
                powerUp.Mass *= (1 - (volumeToSubtract / powerUp.Volume));
                powerUp.Volume -= volumeToSubtract;
                volumeLeftToSubtract -= volumeToSubtract;
            }

            PowerUps.RemoveAll(p => p.Volume <= Mathf.Epsilon);

            return volume - volumeLeftToSubtract;
        }
        
        public Player(float initVolume, float initMass)
        {
            BaseVolume = initVolume;
            _baseMass = initMass;
        }
    }
}