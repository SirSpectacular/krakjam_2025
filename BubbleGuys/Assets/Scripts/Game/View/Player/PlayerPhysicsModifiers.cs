using System.Collections.Generic;
using System.Linq;
using Game.Controller;
using Game.Model;
using UnityEngine;

namespace Game.View.Player
{
    public class PlayerPhysicsModifiers : MonoBehaviour
    {
        [SerializeField] private PhysicsMaterial2D _bouncy;
        [SerializeField] private PhysicsMaterial2D _stiff;
        
        [SerializeField] private int _playerId = 1;
        [SerializeField] private Rigidbody2D _rigidbody;

        public void FixedUpdate()
        {
            SetPhysicsMaterial();
            CalculateMass();
            CalculateBuoyancy();
        }

        private void SetPhysicsMaterial()
        {
            if (DataProvider.Instance.GetPlayer(_playerId).PowerUps.Any(p => p is Rocks))
            {
                _rigidbody.sharedMaterial = _stiff;
            }
            else
            {
                _rigidbody.sharedMaterial = _bouncy;
            }
        }

        private void CalculateMass()
        {
            float mass = DataProvider.Instance.GetPlayer(_playerId).Mass;
            _rigidbody.mass = mass;
        }

        private void CalculateBuoyancy()
        {
            Model.Player player = DataProvider.Instance.GetPlayer(_playerId);
            Vector2 force = player.Volume *
                            PlayerPowerUps.Instance.AirDensity * -Physics2D.gravity + 
                            Physics2D.gravity * 
                            DataProvider.Instance.GetPlayer(_playerId).Mass;
            _rigidbody.AddForce(force * Vector2.up);
        }
    }
}