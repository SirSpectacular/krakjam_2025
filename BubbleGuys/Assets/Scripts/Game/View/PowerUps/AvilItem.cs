using System;
using Game.Controller;
using UnityEngine;
using Game.Player;
using Game.View.Player; 

public class AvilItem : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private float _volume;
    private Animator _animator;
    private bool _isTriggered;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered)
        {
            return;
        }

        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            return;
        }
        _isTriggered = true;
        int playerId = playerMovement._playerId;
        _animator.SetTrigger("Disappear");
        switch (_type)
        {
            case Type.Rocks:
                PlayerPowerUps.Instance.AddRocks(playerId, _volume);
                break;
            case Type.Air:
                PlayerPowerUps.Instance.AddAir(playerId, _volume);
                break;
            case Type.Hellium:
                PlayerPowerUps.Instance.AddHelium(playerId, _volume);
                break;

        }
        StartCoroutine(DestroyAfterAnimation());
    }
    
    private System.Collections.IEnumerator DestroyAfterAnimation()
    {
        float animationDuration = GetAnimationClipLength("AvilPowerUp_disappear");
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
    }

    /// <summary>
    /// Pobiera długość klipu animacji po jego nazwie.
    /// </summary>
    private float GetAnimationClipLength(string clipName)
    {
        if (_animator == null || _animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("[AvilItem] Animator or RuntimeAnimatorController is missing!");
            return 0.5f; // Domyślna długość
        }

        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"[AvilItem] Animation clip '{clipName}' not found!");
        return 0.5f; // Domyślna długość w razie braku animacji
    }

    private enum Type
    {
        Rocks,
        Air,
        Hellium
    }
}
