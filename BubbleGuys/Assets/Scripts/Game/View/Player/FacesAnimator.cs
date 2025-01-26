using System;
using UnityEngine;

namespace Game.View.Player
{
    public class FacesAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private static readonly int LinV = Animator.StringToHash("linV");
        private static readonly int AngV = Animator.StringToHash("angV");
        private static readonly int Bump = Animator.StringToHash("bump");

        private void Awake()
        {
            _animator = transform.GetComponentInChildren<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayerIdProvider idProvider = other.otherCollider.GetComponentInParent<PlayerIdProvider>();
            if (idProvider == null)
            {
                return;
            }
            _animator.SetTrigger(Bump);
        }

        private void Update()
        {
            if (_animator == null)
            {
                return;
            }
            _animator.SetFloat(LinV, _rigidbody2D.linearVelocity.magnitude);
            _animator.SetFloat(AngV, Mathf.Abs(_rigidbody2D.angularVelocity));
        }
    }
}