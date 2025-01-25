using Game.Controller;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;  // ← ważne, żeby dodać to dla IEnumerator

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

        // --------------------------------------------------------------------
        // DODAJEMY PUBLICZNĄ METODĘ: czasowa modyfikacja parametrów "jak kowadło"
        // --------------------------------------------------------------------
        /// <summary>
        /// Na określony czas modyfikuje właściwości Rigidbody2D i współczynniki ruchu,
        /// tak aby postać była cięższa/trudniejsza do zatrzymania.
        /// </summary>
        public IEnumerator ApplyAnvilEffect(
            float duration,
            float massMultiplier,
            float gravityScaleMultiplier,
            float linearDragMultiplier,
            float angularDragMultiplier,
            float moveFactorMultiplier,
            float rotationFactorMultiplier
            // możesz dodać loseVolumeFactorMultiplier, jeśli chcesz to też zmieniać
        )
        {
            // 1) Zapamiętujemy oryginalne wartości
            float originalMass = _rigidbody.mass;
            float originalGravity = _rigidbody.gravityScale;
            float originalDrag = _rigidbody.linearDamping;
            float originalAngularDrag = _rigidbody.angularDamping;

            float originalMoveFactor = _moveFactor;
            float originalRotationFactor = _rotationFactor;
            float originalLoseVolumeFactor = _loseVolumeFactor; // jeśli chcesz zmieniać

            // 2) Modyfikujemy wartości
            _rigidbody.mass *= massMultiplier;
            _rigidbody.gravityScale *= gravityScaleMultiplier;
            _rigidbody.linearDamping *= linearDragMultiplier;
            _rigidbody.angularDamping *= angularDragMultiplier;

            _moveFactor *= moveFactorMultiplier;
            _rotationFactor *= rotationFactorMultiplier;
            // _loseVolumeFactor *= ... (w razie potrzeby)

            // (Opcjonalnie) wyświetl w konsoli, że efekt się zaczął
            Debug.Log($"[PlayerMovement] AnvilEffect START, mass={_rigidbody.mass}, moveFactor={_moveFactor}");

            // 3) Odczekaj "duration" sekund
            yield return new WaitForSeconds(duration);

            // 4) Przywracamy oryginały
            _rigidbody.mass = originalMass;
            _rigidbody.gravityScale = originalGravity;
            _rigidbody.linearDamping = originalDrag;
            _rigidbody.angularDamping = originalAngularDrag;

            _moveFactor = originalMoveFactor;
            _rotationFactor = originalRotationFactor;
            // _loseVolumeFactor = originalLoseVolumeFactor; // w razie potrzeby

            Debug.Log("[PlayerMovement] AnvilEffect END (przywrócone wartości).");
        }
    }
}