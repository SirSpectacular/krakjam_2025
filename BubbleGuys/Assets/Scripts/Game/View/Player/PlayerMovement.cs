using Game.Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _rotationFactor;
        [SerializeField] private float _moveFactor;
        [SerializeField] private float _fartFactor;
        [SerializeField] private float _loseVolumeFactor;
    
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _anusPosition;
        private float _rotationSpeed;
        private Vector2 _moveDirection;
        private bool _isFarting;

        public void OnRotate(InputValue value)
        {
            _rotationSpeed = value.Get<float>();
        }

        public void OnMove(InputValue value)
        {
            _moveDirection = value.Get<Vector2>();
        }
    
        public void OnAttack(InputValue value)
        {
            _isFarting = value.Get<float>() > 0;
        }

        void Update()
        {
            CheckRotation();
            CheckMovement();
            CheckFart();
        }

        private void CheckRotation()
        {
            if (Mathf.Abs(_rotationSpeed) <= Mathf.Epsilon)
            {
                return;
            }

            float torque = _rotationFactor * _rotationSpeed * Time.deltaTime;
            _rigidbody.AddTorque(torque);
        }
    
        private void CheckMovement()
        {
            if (_moveDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return;
            }
        
        
            Vector2 force = _moveDirection * _moveFactor * Time.deltaTime;
            _rigidbody.AddForce(force);
        }
    
        private void CheckFart()
        {
            if (_isFarting == false)
            {
                return;
            }

            Model.Player player = DataProvider.Instance.Player;
            if (player.Volume - Model.Player.MinVolume <= Mathf.Epsilon)
            {
                //Not enough volume to fart;
                return;
            }

            Vector2 fartDirection = -(_anusPosition.position - transform.position).normalized;
            Vector2 force = fartDirection * _fartFactor * Time.deltaTime;
            player.Volume -= Time.deltaTime * _loseVolumeFactor;
            _rigidbody.AddForce(force);
        }
    }
}
