using System.Collections;
using Game.Controller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// ← ważne, żeby dodać to dla IEnumerator

namespace Game.View.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public int _playerId;
        [SerializeField] private float _rotationFactor;
        [SerializeField] private float _moveFactor;
        [SerializeField] private float _fartFactor;
        [SerializeField] private float _loseVolumeFactor;
    
        [SerializeField] private Transform _anusPosition;
        private float _rotationSpeed;
        private Vector2 _moveDirection;
        private bool _isFarting;
        private Rigidbody2D _body;

        public void OnMove(InputValue value)
        {
            _moveDirection = value.Get<Vector2>();
        }
    
        public void OnAttack(InputValue value)
        {
            _isFarting = value.Get<float>() > 0;
        }

        private void Start()
        {
            _body = GetComponentInChildren<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            CheckMovement();
            CheckFart();
        }

        private void CheckRotation()
        {
            if (Mathf.Abs(_rotationSpeed) <= Mathf.Epsilon)
            {
                return;
            }

            float torque = _rotationFactor * _rotationSpeed * Time.fixedDeltaTime;
            _body.AddTorque(torque);
        }
    
        private void CheckMovement()
        {
            if (_moveDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return;
            }

            Vector2 force = _moveDirection * _moveFactor * Time.fixedDeltaTime;
            _body.AddForce(force);
        }

        private void CheckFart()
        {
            if (_isFarting == false)
            {
                return;
            }
            
            Model.Player player = DataProvider.Instance.GetPlayer(_playerId);
            if (player.Volume - player.BaseVolume <= Mathf.Epsilon)
            {
                //Not enough volume to fart;
                return;
            }

            Vector2 fartDirection = -(_anusPosition.position - transform.position).normalized;
            Vector2 force = fartDirection * _fartFactor * Time.fixedDeltaTime;
            float amountToSubtract = Time.fixedDeltaTime * _loseVolumeFactor;
            float subtractedAmount = player.SubtractVolume(amountToSubtract);
            _body.AddForce(force * subtractedAmount / subtractedAmount);
        }
    }
}