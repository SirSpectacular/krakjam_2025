using UnityEngine;
using Game.Player; // Jeśli PlayerMovement znajduje się w namespace Game.Player

public class AvilItem : MonoBehaviour
{
    private Animator _animator;
    private bool _isTriggered = false; // Zabezpieczenie przed wielokrotnym wywołaniem

    [Header("Czas trwania efektu na Playerze")]
    [SerializeField] private float effectDuration = 5f;

    [Header("Mnożniki parametrów PlayerMovement")]
    [SerializeField] private float massMultiplier = 2f;
    [SerializeField] private float gravityScaleMultiplier = 1.5f;
    [SerializeField] private float linearDragMultiplier = 0.5f;
    [SerializeField] private float angularDragMultiplier = 0.5f;
    [SerializeField] private float moveFactorMultiplier = 0.7f;
    [SerializeField] private float rotationFactorMultiplier = 0.5f;

    private void Awake()
    {
        // Pobieramy Animator
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzamy, czy gracz aktywował trigger (np. po tagu lub skrypcie PlayerMovement)
        if (!_isTriggered && collision.CompareTag("Player"))
        {
            _isTriggered = true; // Zabezpieczenie przed wielokrotnym wywołaniem

            // Wywołujemy animację "Disappear"
            if (_animator != null)
            {
                _animator.SetTrigger("Disappear");
            }
            else
            {
                Debug.LogWarning("[AvilItem] Animator is missing on this object!");
            }

            // Modyfikujemy parametry gracza
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartCoroutine(playerMovement.ApplyAnvilEffect(
                    effectDuration,
                    massMultiplier,
                    gravityScaleMultiplier,
                    linearDragMultiplier,
                    angularDragMultiplier,
                    moveFactorMultiplier,
                    rotationFactorMultiplier
                ));
            }
            else
            {
                Debug.LogWarning("[AvilItem] PlayerMovement component not found on the Player!");
            }

            // Usunięcie obiektu po zakończeniu animacji
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    /// <summary>
    /// Coroutine usuwająca obiekt po zakończeniu animacji.
    /// </summary>
    private System.Collections.IEnumerator DestroyAfterAnimation()
    {
        // Czekamy na zakończenie animacji
        float animationDuration = GetAnimationClipLength("AvilPowerUp_disappear");
        yield return new WaitForSeconds(animationDuration);

        // Usuwamy obiekt
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
}
