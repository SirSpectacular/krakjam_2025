using System;
using Game.Controller;
using JetBrains.Annotations;
using UnityEngine;

public class WallOfDeathView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _forwardSpeed;
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _forwardSpeed * Vector2.right;
    }

    [UsedImplicitly]
    public void OnPlayerEntered(int playerId)
    {
        DataProvider.Instance.KillPlayer(playerId);
    }
}
